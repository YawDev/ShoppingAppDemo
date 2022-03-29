using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Shopper.App.Models;
using ShoppingDemo.App.Data.Entites;

namespace ShoppingDemo.App.Services
{
    public interface IUploadService
    {
         void ProcessImage(AddItemModel model, Item item);
         byte[] ToByteArray(IFormFile file);

         void SaveImage(IFormFile file, string path);

         string Upload(IFormFile file );
    }

    public class UploadService : IUploadService
    {
        private readonly IWebHostEnvironment _webHostEnv;

        public UploadService(IWebHostEnvironment webHostEnv)
        {
            _webHostEnv = webHostEnv;
        }

        public void ProcessImage(AddItemModel model, Item item)
        {
            if(model.imageFile?.Length > 0)
            {
                item.Image = ToByteArray(model.imageFile);
                item.FileName = Upload(model.imageFile);
            } 
        }

        public byte[] ToByteArray(IFormFile file)
        {
            using(var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }


        public void SaveImage(IFormFile file, string path)
        {
            using(var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

        }

        public string Upload(IFormFile file )
        {
            var filePath = Path.Combine(_webHostEnv.WebRootPath+"/images", file.FileName);
            SaveImage(file, filePath);
            return file.FileName;
        }
    }
}