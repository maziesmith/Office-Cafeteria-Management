namespace ReactiveWebsocket
{
    public interface ISerializer<in TPayloadType>
    {
        byte[] SerializePayload(TPayloadType payload);
    }
}