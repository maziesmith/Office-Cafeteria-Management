using System.Net.WebSockets;

namespace ReactiveWebsocket.StringWebsocket
{
    public class SimpleReactiveWebsocket : ReactiveWebsocket<string, string>
    {
        public SimpleReactiveWebsocket() : base(new StringSerializer(), new StringDeserializer(), new WebSocketOptions(WebSocketMessageType.Text))
        {

        }
    }
}
