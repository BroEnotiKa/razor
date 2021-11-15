using System;
using BadNews.Repositories.News;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BadNews.Components
{
    public class ArchiveLinksViewComponent : ViewComponent
    {
        private INewsRepository NewsRepository { get; }
        private IMemoryCache MemoryCache { get; }

        public ArchiveLinksViewComponent(INewsRepository newsRepository, IMemoryCache memoryCache)
        {
            NewsRepository = newsRepository;
            MemoryCache = memoryCache;
        }

        public IViewComponentResult Invoke()
        {
            const string cacheKey = nameof(ArchiveLinksViewComponent);

            if (MemoryCache.TryGetValue(cacheKey, out var years)) 
                return View(years);
            
            years = NewsRepository.GetYearsWithArticles();
            if (years != null)
            {
                MemoryCache.Set(cacheKey, years, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });
            }
            return View(years);
        }
    }
}