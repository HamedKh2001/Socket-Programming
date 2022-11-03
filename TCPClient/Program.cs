using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TCPClient
{
    internal class Program
    {
        const int PORT = 6000;
        const string SERVER_IP = "127.0.0.1";
        static EndPoint CurrentUser = null;


        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse(SERVER_IP);
            TcpClient client = new TcpClient();
            client.Connect(ip, PORT);
            CurrentUser = client.Client.LocalEndPoint;
            Console.WriteLine("client connected!!");
            NetworkStream ns = client.GetStream();
            Thread thread = new Thread(o => ReceiveData((TcpClient)o));

            thread.Start(client);

            string s;
            while (!string.IsNullOrEmpty((s = Console.ReadLine())))
            {
                byte[] buffer = Encoding.ASCII.GetBytes(s);
                ns.Write(buffer, 0, buffer.Length);
            }

            client.Client.Shutdown(SocketShutdown.Send);
            thread.Join();
            ns.Close();
            client.Close();
            Console.WriteLine("disconnect from server!!");
            Console.ReadKey();
        }

        static void ReceiveData(TcpClient client)
        {
            if (client.Client.LocalEndPoint != CurrentUser)
            {
                NetworkStream ns = client.GetStream();
                byte[] receivedBytes = new byte[1024];
                int byte_count;

                while ((byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
                {
                    Console.Write(Encoding.ASCII.GetString(receivedBytes, 0, byte_count));
                }
            }
        }
    }
}
