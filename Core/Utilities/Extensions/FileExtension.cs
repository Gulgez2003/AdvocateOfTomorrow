using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Google.Cloud.Storage.V1; // Подключите нужные библиотеки

namespace Core.Utilities.Extensions
{
    public class FileExtension
    {
        private readonly StorageClient _storageClient;

        public FileExtension()
        {
            _storageClient = StorageClient.Create();
        }

        public bool IsImage(IFormFile file)
        {
            return file.ContentType.Contains("image");
        }
            
        public bool IsSizeOk(IFormFile file, int mb)
        {
            return file.Length / 1024 / 1024 < mb;
        }

        public async Task<List<string>> UploadImagesAsync(string bucketName, List<IFormFile> files, string remoteDirectory)
        {
            List<string> remoteImagePaths = new List<string>();

            foreach (var file in files)
            {
                string fileName = $"{Guid.NewGuid()}{file.FileName}";
                string remoteImagePath = Path.Combine(remoteDirectory, fileName);

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    await _storageClient.UploadObjectAsync(bucketName, remoteImagePath, null, memoryStream);
                }

                remoteImagePaths.Add(remoteImagePath);
            }

            return remoteImagePaths;
        }
    }
}
