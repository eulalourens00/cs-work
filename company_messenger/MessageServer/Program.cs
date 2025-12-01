using company_messenger;

namespace MessageServerrrr
{
    internal class Program
    {
        static MessageServer server = new MessageServer();
        static void Main(string[] args)
        {
            Console.Write("Введите порт для прослушивания (5000): ");
            string portStr = Console.ReadLine();
            int port = string.IsNullOrEmpty(portStr) ? 5000 : int.Parse(portStr);

            server.Start(port);
            server.newMessage += (message) =>
            {
                Console.WriteLine($"[{message.Time:HH:mm:ss}] {message.Sender}: {message.Text}");
            };

            Console.WriteLine("Сервер запущен. Нажмите Enter для остановки...");
            Console.ReadLine();

            server.Stop();
        }
    }
}
