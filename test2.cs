// using System;
// using System.Collections.Concurrent;
// using System.Collections.Generic;
// using System.Linq;
// using System.Net.WebSockets;
// using System.Text;
// using System.Threading;
// using System.Threading.Tasks;
// using Newtonsoft.Json;

// namespace NETCORE3.Websocket
// {
//    public class WebSocketHandler 
// {
//     private readonly WebSocketManager _webSocketManager;
//     private readonly ConcurrentDictionary<string, List<WebSocket>> _roomConnections = new ConcurrentDictionary<string, List<WebSocket>>();

//     public WebSocketHandler(WebSocketManager webSocketManager)
//     {
//         _webSocketManager = webSocketManager;
//     }

//     public async Task OnConnected(WebSocket socket, string clientId)
//     {
//         // Hàm này được gọi khi có một kết nối mới được thiết lập
//         // Bạn có thể thực hiện một số thao tác khởi tạo ở đây

//         // Ví dụ: Gửi danh sách các phòng cho client khi kết nối mới được thiết lập
//         var rooms = await GetRooms();
//         var roomListMessage = new { command = "rooms", rooms = rooms };
//         await SendAsync(socket, JsonSerializer.Serialize(roomListMessage));
//     }

//     public async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
//     {
//         var clientId = GetClientId(socket);

//         // Xử lý dữ liệu nhận được từ client
//         var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
//         var data = JsonSerializer.Deserialize<WebSocketMessage>(message);

//         if (data?.Command == "join" && data.RoomId != null)
//         {
//             // Thực hiện yêu cầu tham gia phòng từ client
//             await _webSocketManager.AddToRoomAsync(data.RoomId, socket);

//             // Gửi thông báo cho phòng rằng một thành viên mới đã tham gia
//             var joinMessage = $"Người dùng {clientId} đã tham gia phòng.";
//             await _webSocketManager.SendToRoomAsync(data.RoomId, joinMessage);
//         }
//         else if (data?.Command == "message" && data.RoomId != null)
//         {
//             // Gửi tin nhắn chỉ đến các thành viên trong cùng một phòng
//             var roomMessage = $"[{clientId}]: {data.Content}";
//             await _webSocketManager.SendToRoomAsync(data.RoomId, roomMessage);
//         }
//     }

//     private string GetClientId(WebSocket socket)
//     {
//         // Trích xuất ID của client từ socket (nếu cần thiết)
//         // ...
//         return "client-id"; // Thay thế bằng logic thực tế
//     }

//     private async Task SendAsync(WebSocket socket, string message)
//     {
//         var buffer = Encoding.UTF8.GetBytes(message);
//         await socket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);
//     }

//     private async Task<List<Room>> GetRooms()
//     {
//         // Lấy danh sách các phòng từ database hoặc bất kỳ nguồn dữ liệu nào khác
//         // ...
//         return new List<Room>(); // Thay thế bằng logic thực tế
//     }
// }


// }