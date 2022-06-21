using Microsoft.Extensions.Caching.Memory;

namespace Mentoring.BL
{
    public interface IHddCache : IMemoryCache
    {
        void SaveItem(int id, byte[] imageData);
    }
}
