using Microsoft.AspNetCore.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.ViewComponents
{
    public class GenreMenuViewComponent : ViewComponent
    {
        private readonly MusicStoreEntities _context;

        public GenreMenuViewComponent(MusicStoreEntities context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var genres = _context.Genres.OrderBy(g => g.Name).ToList();
            return View(genres);
        }
    }
}
