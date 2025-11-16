using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System.Text;
namespace UDPclient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            UdpClient receiving = new UdpClient(2024, AddressFamily.InterNetwork);
            Console.WriteLine("Waiting for message");

            while (true)
            {
                UdpReceiveResult result = await receiving.ReceiveAsync();
                byte[] dgram = result.Buffer;

                IPEndPoint from = result.RemoteEndPoint;

                string mess = Encoding.UTF8.GetString(dgram);
                Console.WriteLine($"{from} - {mess}");
            }

            
        }
    }
}
