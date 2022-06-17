using Microsoft.Extensions.Caching.Memory;

namespace Mentoring.BL
{
    public class HddCache : IHddCache
    {
        public ICacheEntry CreateEntry(object key)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public void Remove(object key)
        {
            throw new NotImplementedException();
        }

        public void SaveItem(int id, byte[] imageData)
        {
            using(var stream = new FileStream($"image{id}", FileMode.CreateNew, FileAccess.Write))
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
