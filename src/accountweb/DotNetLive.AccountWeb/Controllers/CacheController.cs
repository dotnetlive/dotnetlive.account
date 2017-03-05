using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace DotNetLive.AccountWeb.Controllers
{
    public class CacheController : Controller
    {
        private IMemoryCache _cache;

        public CacheController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
    }
}
