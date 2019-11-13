using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestZeroMq;

namespace ClientApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            new ClientUser("client").Start();
            Console.ReadLine();
        }
    }
}

public class ClientUser : IClienUserOnMessageRecieved
{
    private const string Endpoint = @"tcp://127.0.0.1:2200";
    private readonly string userName;

    public ClientUser(string userName)
    {
        this.userName = userName;
    }

    public void OnMessageRecievd(string message)
    {
        Console.WriteLine(message);
    }

    public void Start()
    {
        using (var cts = new CancellationTokenSource())
        {
            var cancellationTokenSource = cts;
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cancellationTokenSource.Cancel();
            };

            foreach (var i in Enumerable.Range(1, 5))
            {
                var cancellationToken = cts.Token;
                Task.Run(() => { new Client(userName + i, Endpoint, this, cancellationToken).Start(); }, cts.Token);
            }

          
        }
    }
}