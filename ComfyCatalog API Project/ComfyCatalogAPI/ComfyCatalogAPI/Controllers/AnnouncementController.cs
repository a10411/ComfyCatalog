using Microsoft.AspNetCore.Mvc;

namespace ComfyCatalogAPI.Controllers
{
    public class AnnouncementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
