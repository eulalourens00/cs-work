using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        private void Run()
        {
            Listen().Wait();
        }
        private IList<TcpClient> clients = new List<TcpClient>();

        private async Task Listen()
        {
            TcpListener listen = new TcpListener(IPAddress.Any, 2024);
            listen.Start();
            Console.WriteLine($"Server is listenin and working on 2024 port");
            while (true)
            {
                TcpClient client = await listen.AcceptTcpClientAsync();
                lock(clients)
                    clients.Add(client);
                ListenToClient(client);
            }
        }

        private async void ListenToClient(TcpClient client)
        {
            byte[] buffer = new byte[1024];
            while (true) { 
                int read = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
                string mess = Encoding.UTF8.GetString(buffer,0,read);
                Console.WriteLine(mess);

                await SendToAll(mess);
            }

        }

        private async Task SendToAll(string mess)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(mess);
            List<Task> sendings = new List<Task>();
            List<TcpClient> copy;
            lock (clients) { 
                copy = clients.ToList();
            }

            foreach (TcpClient client in copy) { 
                Task sending = client.GetStream().WriteAsync(bytes,0,bytes.Length); 
                sendings.Add(sending);
            }
            await Task.WhenAll(sendings);
        }
    }
}
