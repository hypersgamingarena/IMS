using Microsoft.AspNetCore.Mvc;

namespace IMS.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
