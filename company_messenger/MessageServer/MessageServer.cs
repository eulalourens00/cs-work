using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading.Tasks.Dataflow;
using company_messenger;
namespace company_messenger
{
    public class MessageServer
    {
        private List<TcpClient> Clients = new List<TcpClient>();
        private TcpListener listener;
        public bool IsRunning = false;

        public event Action<MessageCore> newMessage;

        //1.
        public void Start(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            IsRunning = true;

            Console.WriteLine($"Serber is running at the port - {port}");

            Thread thread = new Thread(ListenForClients);
            thread.Start();
        }
        //2.
        public void ListenForClients()
        {
            while (IsRunning)
            {
                try
                {

                    TcpClient client = listener.AcceptTcpClient();
                    Clients.Add(client);

                    Console.WriteLine("New clieent added");

                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.Start();
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            }
        }

        //3.
        public void HandleClient(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];

                while (client.Connected)
                {
                    int byteRead = stream.Read(buffer, 0, buffer.Length);
                    if (byteRead == 0)
                    {
                        break;
                    }

                    string json = Encoding.UTF8.GetString(buffer, 0, byteRead);

                    var message = new MessageCore("Client", json);
                    newMessage?.Invoke(message);
                    Broadcast(json, client);

                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Clients.Remove(client);
                client.Close();
            }
        }
        //4.
        public void Broadcast(string message, TcpClient sender)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            foreach (var client in Clients.ToArray())
            {
                if (client != sender && client.Connected)
                {
                    try
                    {
                        client.GetStream().Write(data, 0, data.Length);
                    }
                    catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                }
            }
        }
        //5.
        public void Stop()
        {
            IsRunning = false;
            listener?.Stop();
            foreach (var client in Clients)
            {
                client?.Close();
            }
            Clients.Clear();
        }
    }
}
