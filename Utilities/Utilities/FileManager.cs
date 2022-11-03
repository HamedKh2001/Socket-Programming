using Microsoft.AspNetCore.Http;
using ProjectUtilities;

namespace Infrastructure.Services
{
    public class FileManager
    {
        #region DI

        //private readonly IHostingEnvironment _webHostEnvironment;

        #endregion

        #region Ctor

        //public FileManager(IHostingEnvironment webHostEnvironment)
        //{
        //    _webHostEnvironment = webHostEnvironment;
        //}

        #endregion


        #region IFileManager
        public string GetBase64Image(string imagepath)
        {
            using FileStream fs = new FileStream(imagepath, FileMode.Open);
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            var base64 = Convert.ToBase64String(byData);
            return base64;
        }

        //public string GetSavedFilePath(string path)
        //{
        //    path = path.Replace("/", "\\");
        //    return _webHostEnvironment.WebRootPath + path;
        //}

        public string SaveFile(IFormFile File, string SavePath)
        {
            var FileName = Guid.NewGuid() + Path.GetExtension(File.FileName);
            if (SavePath.StartsWith('/'))
            {
                SavePath = SavePath.Substring(1, SavePath.Length - 1);
            }
            var folderpath = Path.Combine(Directory.GetCurrentDirectory(), SavePath.Replace("/", "\\"));
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }
            var fullpath = Path.Combine(folderpath, FileName);
            using var stream = new FileStream(fullpath, FileMode.Create);
            File.CopyTo(stream);
            return FileName;
        }

        public OperationResault DeleteFile(string filePath, string fileName)
        {
            try
            {
                File.Delete(filePath + fileName);
                return OperationResault.Success();
            }
            catch (Exception ex)
            {
                return OperationResault.Error(ex.Message);
            }
        }

        public byte[] GetByteArray(IFormFile image)
        {
            byte[] p1 = null;
            using (var fs1 = image.OpenReadStream())
            using (var ms1 = new MemoryStream())
            {
                fs1.CopyTo(ms1);
                p1 = ms1.ToArray();
            }
            return p1;
        }

        #endregion

    }
}
