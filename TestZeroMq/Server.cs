using System.Linq;
using System.Threading;
using fszmq;

namespace TestZeroMq
{
    public class Server
    {
        private readonly string endpoint;
        private readonly CancellationToken token;

        public Server(CancellationToken token, string endpoint)
        {
            this.token = token;
            this.endpoint = endpoint;
        }

        public void Start()
        {
            using (var context = new Context())
            {
                var socket = context.Rep();
                socket.Bind(endpoint);

                while (!token.IsCancellationRequested)
                {
                    var msg = socket.Recv();
                    if (!Common.Ping.SequenceEqual(msg)) continue;
                    Thread.Sleep(1000);
                    socket.Send(Common.Pong);
                }
            }
        }
    }
}