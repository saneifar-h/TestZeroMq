using System;
using System.Threading;
using System.Threading.Tasks;
using TestZeroMq;

namespace ServerApp
{
    public static class Program
    {
        private const string Endpoint = @"tcp://127.0.0.1:2200";

        private static void Main(string[] args)
        {
            using (var cts = new CancellationTokenSource())
            {
                var cancellationTokenSource = cts;
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true;
                    cancellationTokenSource.Cancel();
                };
                var cancellationToken = cts.Token;
                // spawn server
                Task.Run(() => new Server(cancellationToken, Endpoint).Start(), cancellationToken)
                    .Wait(cancellationToken);
            }
        }
    }
}

