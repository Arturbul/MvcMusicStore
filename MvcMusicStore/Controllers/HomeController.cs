using Microsoft.AspNetCore.Mvc;
using MvcMusicStore.Models;
using System.Diagnostics;

namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MusicStoreEntities _storeDB;

        public HomeController(MusicStoreEntities storeDB, ILogger<HomeController> logger)
        {
            _storeDB = storeDB;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var albums = GetTopSellingAlbums(5);
            return View(albums);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private IList<Album> GetTopSellingAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count
            return _storeDB.Albums
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(count)
                .ToList();
        }
    }
}
