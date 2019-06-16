namespace ReactiveWebsocket
{
    public interface IDeserializer<out TPayloadType>
    {
        TPayloadType DeserializePayload(byte[] bytes);
    }
}