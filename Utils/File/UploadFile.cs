using Library.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Library.Utils.File
{
    public class UploadFile
    {
        public static string UploadImage(IFormFile file, string webRootPath, string folder, string host)
        {
            if (file == null || file.Length == 0)
            {
                throw new NoFileException();
            }

            var fileName = Path.GetFileName(file.FileName);
            var fileExtension = Path.GetExtension(fileName);

            if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".webp" && fileExtension != ".png")
            {
                throw new InvalidFileException();
            }

            // Generate a new file name with a GUID (UUID) to avoid conflicts
            fileName = Guid.NewGuid() + fileExtension;

            var filePath = Path.Combine(webRootPath, folder, fileName);

            // Ensure the directory exists
            var directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            // Form the URL by replacing the webRootPath with the host URL
            var relativePath = filePath.Replace(webRootPath, "").Replace("\\", "/");
            return $"{host}{relativePath}";
        }
    }
}