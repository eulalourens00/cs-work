using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace upd_meow
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await new Program().Run();
        }

        private async Task Run()
        {
            UdpClient server = new UdpClient(2024);
            Console.WriteLine("");

            IList<IPEndPoint> clients = new List<IPEndPoint>();
            while (true)
            {
                UdpReceiveResult recieved;
                try
                {
                    recieved = await server.ReceiveAsync();
                }

                catch (SocketException) { continue; }



                IPEndPoint from = recieved.RemoteEndPoint;
                byte[] datagram = recieved.Buffer;

                if (!clients.Contains(from)) { 
                    clients.Add(from);
                    Console.WriteLine($"Add client {from}");
                }

                foreach (IPEndPoint client in clients)
                {
                    try
                    {
                        await server.SendAsync(datagram, client);
                    }
                    catch (SocketException) 
                    { 
                        Console.WriteLine($"Delete clietn"); 
                    }

                    catch (Exception ex)
                    { 
                        Console.WriteLine(ex.Message); 
                    }
                }
            }


        }
    }
}
