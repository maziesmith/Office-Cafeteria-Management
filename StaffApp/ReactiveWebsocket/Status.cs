using System;

namespace ReactiveWebsocket
{
    public class Status
    {
        public ConnectionState ConnectionState { get; set; }

        public string  Message{ get; set; }

        public Exception Error { get; set; }
    }
}