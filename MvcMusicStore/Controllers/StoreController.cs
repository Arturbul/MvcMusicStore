using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace MvcMusicStore.Controllers
{
    public class StoreController : Controller
    {
        // GET: /Store/
        public string Index()
        {
            return "Hello from Store.Index()";
        }
        //
        // GET: /Store/Browse
        public string Browse(string genre)
        {
            string message = HttpUtility.HtmlEncode($"Store.Browse, Genre= {genre}");
            return message;
        }
        //
        // GET: /Store/Details
        public string Details(int id)
        {
            return $"Store.Details, ID ={id}";
        }
    }
}
