using HTTPPublisher.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace HTTPPublisher.Controllers
{
    public class HomeController : Controller
    {
        protected readonly HttpClient _httpClient;
        protected readonly FileManager _fileManager;

        public HomeController()
        {
            _httpClient = new HttpClient();
            _fileManager = new FileManager();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Send()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Send(IFormFile formFile)
        {
            var fileBytes = _fileManager.GetByteArray(formFile);
            var req = new MultipartFormDataContent();
            var imageContent = new ByteArrayContent(fileBytes);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpg");
            imageContent.Headers.ContentLength = fileBytes.Length;
            req.Add(imageContent, "OsPhoto", formFile.FileName);
            try
            {
                await _httpClient.PostAsync("http://localhost:5227/home/Receive", req);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }

            return View(nameof(HomeController.Resault), "Successfully Sent :D");
        }

        public IActionResult Resault(string content)
        {
            return View(content);
        }
    }
}