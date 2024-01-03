using System.Net.WebSockets;
using System.Text;
using Constant;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseWebSockets();
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/ocpp" || context.Request.Path == "/ocpp1"  )
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            // .Add(webSocket);
            if (ConstantClass._clients.TryGetValue(context.Request.Path, out var sockets))
            {
                sockets.Add(webSocket);
            }
            else
            {
                ConstantClass._clients.TryAdd(context.Request.Path, new List<WebSocket> { webSocket });
            }
            await HandleWebSocket(webSocket, context.Request.Path);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
    else
    {
        await next(context);
    }

});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
static async Task HandleWebSocket(WebSocket webSocket, string chanel)
    {
      try
      {
        byte[] buffer = new byte[1024];

        while (webSocket.State == WebSocketState.Open)
        {
          WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

          if (result.MessageType == WebSocketMessageType.Text)
          {
            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
            Console.WriteLine($"Received message: {receivedMessage}");

            // Xử lý tin nhắn nhận được từ client

            // Ví dụ: Gửi lại tin nhắn cho client
            // string responseMessage = "Server received your message.";
            // byte[] responseBuffer = Encoding.UTF8.GetBytes(responseMessage);
            
            //await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);
            foreach (var client in ConstantClass._clients[chanel])
            {
                if (client.State == WebSocketState.Open)
                {
                    await client.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
          }
          else if (result.MessageType == WebSocketMessageType.Close)
          {
            Console.WriteLine("WebSocket connection closed.");
            // ConstantClass._clients.Remove(webSocket);
             if (ConstantClass._clients.TryGetValue(chanel, out var sockets))
            {
                sockets.Remove(webSocket);

                // Remove the room if there are no more sockets in it
                if (sockets.Count == 0)
                {
                    ConstantClass._clients.TryRemove(chanel, out _);
                }
            }
            break;
          }
        }
      }
      catch (WebSocketException ex)
      {
        // Xử lý các lỗi WebSocketException
        Console.WriteLine($"WebSocketException: {ex.Message}");
        if (ex.InnerException != null)
        {
          Console.WriteLine($"InnerException: {ex.InnerException.Message}");
        }
      }
      finally
      {
        // Đóng kết nối WebSocket khi kết thúc
        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
      }
    }