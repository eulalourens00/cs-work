using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace extra
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            try
            {
                await socket.ConnectAsync("127.0.0.1", 8888);
                Console.WriteLine($"Connected {socket.RemoteEndPoint} complite");
            }
            catch (SocketException) {
                Console.WriteLine($"{socket.RemoteEndPoint} defeat");
            }
        }
    }
}
