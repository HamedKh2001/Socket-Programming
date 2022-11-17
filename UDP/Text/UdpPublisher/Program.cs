using System.Net.Sockets;
using System.Text;

namespace UdpPublisher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UdpClient udpClient = new();
            try
            {
                while (true)
                {
                    udpClient.Connect("127.0.0.1", 11000);

                    Console.WriteLine("Your Message?");

                    var input = Console.ReadLine();  

                    var base_2 = Convert.ToString(48,2);
                    var x = Encoding.UTF8.GetBytes("آ");
                    var sendBytes = Encoding.ASCII.GetBytes(input);

                    udpClient.Send(sendBytes, sendBytes.Length);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
        }
    }
}