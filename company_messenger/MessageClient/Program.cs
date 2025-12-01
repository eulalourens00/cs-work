
using company_messenger;

namespace MessageClient
{
    internal class Program
    {
        static MessageClientcs client = new MessageClientcs();

        static void Main(string[] args)
        {
            Console.Write("Введите IP сервера (localhost): ");
            string ip = Console.ReadLine();
            if (string.IsNullOrEmpty(ip)) ip = "127.0.0.1";

            Console.Write("Введите порт (5000): ");
            string portStr = Console.ReadLine();
            int port = string.IsNullOrEmpty(portStr) ? 5000 : int.Parse(portStr);

            bool connected = client.Connect(ip, port);
            if (!connected)
            {
                Console.WriteLine("Не удалось подключиться к серверу");
                return;
            }

            Console.WriteLine("Подключено/ Введите сообщения:");

            client.MessageReceived += (message) =>
            {
                Console.WriteLine($"Получено: {message}");
            };

            while (true)
            {
                string input = Console.ReadLine();
                if (input.ToLower() == "exit") break;

                client.SendMessage(input);
            }

            client.Disconnect();
        }
    }
}
