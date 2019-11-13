using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestZeroMq;

namespace ConsoleApp1
{
    internal class Program
    {
        private const string Endpoint = @"tcp://127.0.0.1:2200";

        private static void Main(string[] args)
        {
            using (var cts = new CancellationTokenSource())
            {
                var tokenSource = cts;
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true;
                    tokenSource.Cancel();
                };

                foreach (var i in Enumerable.Range(1, 5))
                {
                    var token = cts.Token;
                    Task.Run(() =>
                    {
                        var client = new Client(i, token, Endpoint);
                        client.Start();
                        client.MessageRecieved += (obj, str) => { Console.WriteLine(str); };
                    }, token);
                }
            }
        }
    }
}