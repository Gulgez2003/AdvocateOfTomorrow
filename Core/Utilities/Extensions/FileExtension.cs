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
        public async Task<string> UploadImageAsync(string bucketName, IFormFile file, string remoteDirectory)
        {
            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            string remoteImagePath = Path.Combine(remoteDirectory, fileName);

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                await _storageClient.UploadObjectAsync(bucketName, remoteImagePath, null, memoryStream);
            }

            return remoteImagePath;
        }
    }
}
