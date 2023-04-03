using Microsoft.AspNetCore.Mvc;

namespace ComfyCatalogAPI.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
