using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
namespace UDP
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using UdpClient sending = new UdpClient();
            sending.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"),
                2024));
            while (true)
            {
                Console.WriteLine("Enter ur message - ");
                string message = Console.ReadLine();

                await sending.SendAsync(Encoding.UTF8.GetBytes(message));
                Console.WriteLine("Message is send");

                //meow meow meow meow meow 


            }

        }
    }
}
