using Microsoft.Extensions.Caching.Memory;

namespace Mentoring.BL
{
    public class HddCache : IHddCache
    {
        private readonly IConfiguration _configuration;
        public HddCache(IConfiguration configuration)
        {
            _configuration = configuration;
            CheckCacheExpiration();
        }

        private void CheckCacheExpiration()
        {
            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory()))
            {
                if (file.StartsWith("image"))
                {
                    var expirationSpan = double.Parse(_configuration.GetSection("CacheExpiration").Value) * -1;
                    Console.WriteLine("expiration span {0}", expirationSpan);
                    var fileInfo = new FileInfo(file);
                    var creationDate = fileInfo.CreationTimeUtc;
                    if (DateTime.UtcNow.AddMinutes(expirationSpan) > creationDate)
                    {
                        File.Delete(file);
                    }
                }
            }
        }

        public ICacheEntry CreateEntry(object key)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public void Remove(object key)
        {
            if (key != null && File.Exists($"image{key}"))
            {
                File.Delete($"image{key}");
            }
        }

        public void SaveItem(int id, byte[] imageData)
        {
            var fileNames = Directory.GetFiles(Directory.GetCurrentDirectory());
            var imagesCount = fileNames.Where(x => x.StartsWith("image")).Count();
            if (imagesCount >= int.Parse(_configuration.GetSection("MaxImagesOnCache").Value))
            {
                DateTime minDate = DateTime.MinValue;
                string? oldestFileName = null;
                foreach (var fileName in fileNames)
                {
                    var fileInfo = new FileInfo(fileName);
                    if (fileInfo.CreationTimeUtc < minDate)
                    {
                        minDate = fileInfo.CreationTimeUtc;
                        oldestFileName = fileName;
                    }
                }
                if (!string.IsNullOrEmpty(oldestFileName))
                {
                    File.Delete(oldestFileName);
                }
            }
            using (var stream = new FileStream($"image{id}", FileMode.CreateNew, FileAccess.Write))
            {
                stream.Write(imageData, 0, imageData.Length);
            }
        }

        public bool TryGetValue(object key, out object value)
        {
            int fileId;
            if (int.TryParse(key.ToString(), out fileId) && File.Exists($"image{fileId}"))
            {
                value = File.ReadAllBytes($"image{fileId}");
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }
    }
}
