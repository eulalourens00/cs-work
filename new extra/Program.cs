using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
namespace new_extra
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var MySocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            var server = "ya.ru";
            MySocket.Connect(server, 80);

            using var stream = new NetworkStream(MySocket);

            Console.WriteLine($"Local ip: {stream.Socket.LocalEndPoint}");
            Console.WriteLine($"Server ip: {stream.Socket.RemoteEndPoint}");

            var mess = $"GET / HTTP/1.1\r\nHost: {server}\r\nConnection: Close \r\n\r\n";
            var data = Encoding.UTF8.GetBytes( mess );
            await stream.WriteAsync(data);
            Console.WriteLine("data send server");
        }
    }
}
