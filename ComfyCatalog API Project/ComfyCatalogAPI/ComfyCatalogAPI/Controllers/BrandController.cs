using Microsoft.AspNetCore.Mvc;

namespace ComfyCatalogAPI.Controllers
{
    public class BrandController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
