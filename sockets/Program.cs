using System.Net.Sockets;
using System.Net;
namespace sockets
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //IPAddress ip = IPAddress.Parse("127.0.0.1");
            //IPAddress.Loopback; // хранит в себе 127.0.0.1
            //IPAddress.Broadcast; // 
            //Console.WriteLine(ip.AddressFamily);

            //IPEndPoint endPoint = new IPEndPoint(ip, 8080);
            //Console.WriteLine(endPoint);

            //string uri = "https://ya.ru";
            //Uri myuri = new Uri(uri);

            //var yandex = await Dns.GetHostEntryAsync("yandex.ru");
            //Console.WriteLine(yandex.HostName);
            //foreach(var ip in yandex.AddressList) { Console.WriteLine(ip); }

            Socket socket = new Socket
                (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //Accept() - принятие
            // Bind() - соединение с IpEndPoint
            // Close() - закрыть соединения
            // Connect() - присоединиться
            // Listen() - запустить пассивный сокет
            // Send() - отправка
            // SocketShutdown -> Send -> Receive -> Both -
            //                   блокирует отправку -> блокирует получение -> бокирует все

            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint ep = new IPEndPoint(ip, 1024);
            socket.Bind(ep);
            socket.Listen(10); //10 - может подключиться 10 человек

            try
            {
                while (true)
                {
                    Socket ns = socket.Accept();
                    Console.WriteLine(ns.RemoteEndPoint.ToString());
                    ns.Send(System.Text.Encoding.ASCII.GetBytes
                        (DateTime.Now.ToString()));
                    ns.Shutdown(SocketShutdown.Both);
                    ns.Close();
                }
            }
            catch(SocketException ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
