using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReactiveWebsocket
{
    public interface IReactiveWebsocket<in TRequestPayload, TResponsePayload> : IDisposable
    {
        /// <summary>
        /// Status stream for the websocket connection
        /// </summary>
        IObservable<Status> StatusStream { get; }


        /// <summary>
        /// Connects to the websocket server
        /// </summary>
        /// <param name="uri">uri to connect to</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        Task ConnectAsync(Uri uri, CancellationToken cancellationToken);

        /// <summary>
        /// Reconnect to the given websocket server. Previous connection is disposed
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Reconnect(Uri uri, CancellationToken cancellationToken);


        /// <summary>
        /// Gets a single response identified by given predicate condition
        /// </summary>
        /// <param name="filter">The filter predicate to identify the response</param>
        /// <returns></returns>
        Task<TResponsePayload> GetResponse(Predicate<TResponsePayload> filter);


        /// <summary>
        /// Sends a request and gets a single response identified by given predicate condition
        /// </summary>
        /// <param name="requestPayload"></param>
        /// <param name="filter">The filter predicate to identify the response</param>
        /// <returns></returns>
        Task<TResponsePayload> GetResponse(TRequestPayload requestPayload, Predicate<TResponsePayload> filter);


        /// <summary>
        /// Sends a request and gets a stream of response identified by given predicate condition
        /// </summary>
        /// <param name="requestPayload"></param>
        /// <param name="filter">The filter predicate to identify the response</param>
        /// <returns></returns>
        IObservable<TResponsePayload> GetObservable(TRequestPayload requestPayload, Predicate<TResponsePayload> filter);


        /// <summary>
        /// Sends a request and gets a stream of response identified by given predicate condition
        /// </summary>
        /// <param name="filter">The filter predicate to identify the response</param>
        /// <returns></returns>
        IObservable<TResponsePayload> GetObservable(Predicate<TResponsePayload> filter);
    }
}