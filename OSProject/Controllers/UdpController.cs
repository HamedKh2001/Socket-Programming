using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace OSProject.Controllers
{
    public class UdpController : Controller
    {
        public const string Text = "this is a test";

        public UdpController()
        {
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Send()
        {
            var udpClient = new UdpClient("127.0.0.1",9050);
            var data = Encoding.ASCII.GetBytes(Text);
            try
            {
                udpClient.Send(data, data.Length);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
            return View();
        }
    }
}
