// using System;
// using System.Collections.Concurrent;
// using System.Collections.Generic;
// using System.Linq;
// using System.Net.WebSockets;
// using System.Text;
// using System.Threading;
// using System.Threading.Tasks;

// namespace NETCORE3.Websocket
// {
//     public class WebSocketManager
//     {
//         private readonly ConcurrentDictionary<string, List<WebSocket>> _rooms = new ConcurrentDictionary<string, List<WebSocket>>();

//         public async Task AddToRoomAsync(string roomId, WebSocket socket)
//         {
//             if (_rooms.TryGetValue(roomId, out var sockets))
//             {
//                 sockets.Add(socket);
//             }
//             else
//             {
//                 _rooms.TryAdd(roomId, new List<WebSocket> { socket });
//             }
//         }

//         public async Task RemoveFromRoomAsync(string roomId, WebSocket socket)
//         {
//             if (_rooms.TryGetValue(roomId, out var sockets))
//             {
//                 sockets.Remove(socket);

//                 // Remove the room if there are no more sockets in it
//                 if (sockets.Count == 0)
//                 {
//                     _rooms.TryRemove(roomId, out _);
//                 }
//             }
//         }

//         public async Task SendToRoomAsync(string roomId, string message)
//         {
//             if (_rooms.TryGetValue(roomId, out var sockets))
//             {
//                 var tasks = sockets.Select(socket => SendAsync(socket, message));
//                 await Task.WhenAll(tasks);
//             }
//         }

//         private async Task SendAsync(WebSocket socket, string message)
//         {
//             var buffer = Encoding.UTF8.GetBytes(message);
//             await socket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);
//         }
//     }

// }