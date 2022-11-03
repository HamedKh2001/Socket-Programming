using System.Net;
using System.Net.Sockets;
using System.Text;

IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
UdpClient newsock = new UdpClient(ipep);


IPEndPoint ReceiverIPEndpoint = new IPEndPoint(IPAddress.Any, 7061);

while (true)
{
    byte[] data = newsock.Receive(ref ReceiverIPEndpoint);
    string test = Encoding.ASCII.GetString(data);
    Console.WriteLine($"data = {test}");
}
Console.ReadKey();