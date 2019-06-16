using System.Text;

namespace ReactiveWebsocket.StringWebsocket
{
    public class StringDeserializer : IDeserializer<string>
    {
        public string DeserializePayload(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}