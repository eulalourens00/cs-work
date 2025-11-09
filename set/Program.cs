using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
namespace set
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 8888);
            using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            socket.Bind(iPEndPoint);

            socket.Listen(1000);

            Console.WriteLine("Server start");
            using Socket client = await socket.AcceptAsync();
            Console.WriteLine($"IP client - {client.RemoteEndPoint}");

            //Console.WriteLine(socket.LocalEndPoint);
        }
    }
}
