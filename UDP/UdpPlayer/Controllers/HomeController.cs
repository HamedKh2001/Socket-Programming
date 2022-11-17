using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;

namespace Receiver.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        public FileManager fileManager { get; set; }
        public HomeController()
        {
            fileManager = new FileManager();
        }

        [HttpPost]
        public async Task<IActionResult> Send(IFormFile formFile, CancellationToken cancellationToken, int packetSize = 1316) //VLC Packet Size
        {
            await Task.Run(async () => await StartSending(formFile, packetSize), cancellationToken);
            return Ok();
        }

        private async Task StartSending(IFormFile formFile, int packetSize)
        {
            var udpClient = new UdpClient("127.0.0.1", 9050);
            var byteArr = fileManager.GetByteArray(formFile);
            var chunks = byteArr.Chunk(packetSize);
            int numberOfSentPackets = 0;

            foreach (var packet in chunks)
            {
                try
                {
                    await udpClient.SendAsync(packet, packet.Length);
                    Task.Delay(18).Wait();
                    numberOfSentPackets++;
                    Console.WriteLine($"{numberOfSentPackets} Packets Sent");
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
