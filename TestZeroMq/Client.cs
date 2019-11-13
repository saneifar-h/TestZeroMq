using System.Linq;
using System.Threading;
using fszmq;

namespace TestZeroMq
{
    public interface IClienUserOnMessageRecieved
    {
        void OnMessageRecievd(string message);
    }

    public sealed class Client
    {
        private readonly IClienUserOnMessageRecieved clienUserOnMessags;
        private readonly string endpoint;
        private readonly string identifier;
        private readonly CancellationToken token;

        public Client(string identifier, string endpoint, IClienUserOnMessageRecieved clienUserOnMessags,
            CancellationToken token)
        {
            this.identifier = identifier;
            this.endpoint = endpoint;
            this.clienUserOnMessags = clienUserOnMessags;
            this.token = token;
        }

        public void Start()
        {
            using (var context = new Context())
            {
                var socket = context.Req();
                socket.Connect(endpoint);
                while (!token.IsCancellationRequested)
                {
                    socket.Send(Common.Ping);
                    var msg = socket.Recv();
                    if (Common.Pong.SequenceEqual(msg)) clienUserOnMessags.OnMessageRecievd($"({identifier}) got ping");
                }
            }
        }
    }
}