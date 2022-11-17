using System.Net;
using System.Net.Sockets;
using System.Text;

Console.ForegroundColor = ConsoleColor.Green;
IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 11000);
UdpClient newSocket = new UdpClient(ipep);

IPEndPoint ReceiverIPEndpoint = new IPEndPoint(IPAddress.Any, 11000);

while (true)
{
    byte[] data = newSocket.Receive(ref ReceiverIPEndpoint);
    string test = Encoding.ASCII.GetString(data);
    Console.WriteLine($"Received:\t{DateTime.Now.ToShortTimeString()}\t{test}");
}
Console.ReadKey();