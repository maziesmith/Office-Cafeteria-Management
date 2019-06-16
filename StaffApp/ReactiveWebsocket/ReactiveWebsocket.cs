using System;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;

namespace ReactiveWebsocket
{
    public class ReactiveWebsocket<TRequestPayload, TResponsePayload> : IReactiveWebsocket<TRequestPayload, TResponsePayload>
    {
        private readonly ISerializer<TRequestPayload> _serializer;
        private readonly IDeserializer<TResponsePayload> _deserializer;
        private readonly WebSocketOptions _options;
        private ClientWebSocket _webSocket;
        private Subject<TResponsePayload> _dataStream;
        private BehaviorSubject<Status> _statusStream;

        public ReactiveWebsocket(ISerializer<TRequestPayload> serializer, IDeserializer<TResponsePayload> deserializer, WebSocketOptions options)
        {
            _serializer = serializer;
            _deserializer = deserializer;
            _options = options;
            Initialize();
        }

        public IObservable<Status> StatusStream => _statusStream;

        public async Task ConnectAsync(Uri uri, CancellationToken cancellationToken)
        {
            PublishStatus(ConnectionState.Connecting, $"Connecting to {uri}");

            try
            {
                await _webSocket.ConnectAsync(uri, cancellationToken);
                StartListening();
                PublishStatus(ConnectionState.Connected, $"Connected to {uri}");
            }
            catch (Exception exception)
            {
                PublishStatus(ConnectionState.Disconnected, exception);
            }
        }

        public async Task Reconnect(Uri uri, CancellationToken cancellationToken)
        {
            Dispose();
            PublishStatus(ConnectionState.Connecting, $"Reconnecting to {uri}");
            Initialize();
            await ConnectAsync(uri, cancellationToken);
        }

        public Task<TResponsePayload> GetResponse(Predicate<TResponsePayload> filter)
        {
            return _dataStream.Where(payLoad => filter(payLoad))
                 .FirstAsync().ToTask();
        }

        public async Task<TResponsePayload> GetResponse(TRequestPayload requestPayload, Predicate<TResponsePayload> filter)
        {
            await SendRequestAsync(requestPayload);
            var reponse = await _dataStream.Where(payLoad => filter(payLoad))
                   .FirstAsync().ToTask();
            return reponse;
        }

        public IObservable<TResponsePayload> GetObservable(TRequestPayload requestPayload, Predicate<TResponsePayload> filter)
        {
            return Observable.Create<TResponsePayload>(observer =>
            {
                SendRequestAsync(requestPayload)
                    .ContinueWith(task =>
                    {
                        if (task.IsFaulted)
                            throw new Exception(task.Exception.Message);
                    });
                return _dataStream.Where(payLoad => filter(payLoad))
                 .Subscribe(observer);
            });
        }

        public IObservable<TResponsePayload> GetObservable(Predicate<TResponsePayload> filter)
        {
            return _dataStream.Where(payLoad => filter(payLoad));
        }


        public void Dispose()
        {
            _webSocket?.Dispose();
            _dataStream?.Dispose();
            _statusStream.Dispose();
        }

        #region Private

        private void Initialize()
        {
            _webSocket = new ClientWebSocket();
            if (_options.KeepAliveInterval.HasValue)
                _webSocket.Options.KeepAliveInterval = _options.KeepAliveInterval.Value;
            _dataStream = new Subject<TResponsePayload>();
            _statusStream = new BehaviorSubject<Status>(new Status());
        }


        private void PublishStatus(ConnectionState state, string message)
        {
            PublishStatus(state, message, null);
        }

        private void PublishStatus(ConnectionState state, Exception exception)
        {
            PublishStatus(state, "", exception);
        }

        private void PublishStatus(ConnectionState state, string message, Exception exception)
        {
            _statusStream.OnNext(new Status
            {
                ConnectionState = state,
                Message = message,
                Error = exception
            });
        }

        private async Task SendRequestAsync(TRequestPayload requestPayload)
        {
            var bytes = _serializer.SerializePayload(requestPayload);
            await _webSocket.SendAsync(new ArraySegment<byte>(bytes), _options.MessageType, true,
                CancellationToken.None);
        }

        //async void is bad but there is no other option in this scenario. 
        //It is equivalent to event handler, caller is not interested in task
        private async void StartListening()
        {
            var receiveBuffer = new byte[4096 * 20];
            while (_webSocket.State == WebSocketState.Open)
            {
                var totalBytes = new byte[0];
                WebSocketReceiveResult result;
                do
                {
                    result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                    }
                    else
                    {
                        var existingSize = totalBytes.Length;
                        totalBytes = new byte[existingSize + result.Count];
                        Buffer.BlockCopy(receiveBuffer, 0, totalBytes, existingSize, result.Count);
                    }

                } while (!result.EndOfMessage);

                var payLoad = _deserializer.DeserializePayload(totalBytes);
                _dataStream.OnNext(payLoad);
            }
        }

        #endregion

    }
}
