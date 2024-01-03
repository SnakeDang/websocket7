using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.WebSockets;

namespace Constant
{
    public static class ConstantClass
    {
        public static object Content {get;set;}
        public static ConcurrentDictionary<string, List<WebSocket>> _clients {get;set;} = new ConcurrentDictionary<string, List<WebSocket>>();
    }
}