using System.Text;

namespace ReactiveWebsocket.StringWebsocket
{
    public class StringSerializer : ISerializer<string>
    {
        public byte[] SerializePayload(string payload)
        {
            return Encoding.UTF8.GetBytes(payload);
        }
    }
}
