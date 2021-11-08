using BadNews.Repositories.News;
using Microsoft.AspNetCore.Mvc;

namespace BadNews.Components
{
    public class ArchiveLinksViewComponent : ViewComponent
    {
        private INewsRepository NewsRepository { get; }

        public ArchiveLinksViewComponent(INewsRepository newsRepository)
        {
            NewsRepository = newsRepository;
        }

        public IViewComponentResult Invoke()
        {
            var years = NewsRepository.GetYearsWithArticles();
            return View(years);
        }
    }
}