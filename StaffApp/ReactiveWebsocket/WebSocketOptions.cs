using System;
using System.Net.WebSockets;

namespace ReactiveWebsocket
{
    public class WebSocketOptions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageType"> The type of message your payload supports(binary/text)</param>
        public WebSocketOptions(WebSocketMessageType messageType)
        {
            MessageType = messageType;
        }

        /// <summary>
        /// Time interval to keep the connection alive after idle state. Typically it is 30 seconds,
        //  So the client sends a pong request 30 seconds after being idle.
        /// </summary>
        public TimeSpan? KeepAliveInterval { get; set; }

        /// <summary>
        /// The type of message your payload supports (binary/text)
        /// </summary>
        public WebSocketMessageType MessageType { get; set; }


        /// <summary>
        /// If set to true, client will try to autoconnect to disconnection
        /// </summary>
        public bool Autoreconnect { get; set; }

    }
}