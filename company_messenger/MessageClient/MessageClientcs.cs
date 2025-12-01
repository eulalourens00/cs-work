using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace company_messenger
{
    public class MessageClientcs
    {
        private TcpClient client;
        private Thread listeningThread;
        private bool isConnected = false;

        public event Action<string> MessageReceived;

        public bool Connect(string serverIp, int port)
        {
            try
            {
                client = new TcpClient();
                client.Connect(serverIp, port);
                isConnected = true;

                listeningThread = new Thread(ListenForMessages);
                listeningThread.Start();

                return true;
            }
            catch
            {
                return false;
            }
        }
        //1.
        private void ListenForMessages()
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];

                while (isConnected)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    MessageReceived?.Invoke(message);
                }
            }
            catch { }
        }

        //2.
        public void SendMessage(string message)
        {
            if (!isConnected || client == null) return;

            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
            }
            catch { }
        }

        public void Disconnect()
        {
            isConnected = false;
            client?.Close();
            listeningThread?.Join();
        }
    }
}
