using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
namespace new_new_wxtrs
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using TcpClient client = new TcpClient();
            try
            {
                await client.ConnectAsync("www.ya.ru", 80);
                Console.WriteLine("connected");
            }
            catch (SocketException e) { Console.WriteLine(e.Message); };

            client.Close();
            Console.WriteLine(client.Connected);
        }
    }
}
