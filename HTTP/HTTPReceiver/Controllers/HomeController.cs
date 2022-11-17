using HTTPReceiver.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace HTTPReceiver.Controllers
{
    public class HomeController : Controller
    {
        private readonly FileManager _fileManager;
        private string SavePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);

        public HomeController()
        {
            _fileManager = new FileManager();
        }

        public IActionResult Index()
        {
            var files = Directory.GetFiles(SavePath);
            List<string> imageFiles = new List<string>();
            foreach (var filename in files)
            {
                if (Regex.IsMatch(filename.ToLower(), @"\.jpg$|\.png$|\.gif$|\.jpeg"))
                {
                    var splitArr = filename.Split('\\');
                    imageFiles.Add(splitArr[splitArr.Length-1]);
                }
            }
            return View(imageFiles);
        }

        [HttpPost]
        public IActionResult Receive(MultipartFormDataContent dataContent)
        {
            var recievedFile = HttpContext.Request.Form.Files.FirstOrDefault();
            var fileName = _fileManager.SaveFile(recievedFile, SavePath);
            var receivedContent = new ReceivedContent
            {
                FilePath = $"ReceivedPhotos/{fileName}",
                FileName = recievedFile.FileName,
                HostPublisher = HttpContext.Request.Host.Value,
            };
            return View(receivedContent);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}