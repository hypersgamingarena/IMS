using Microsoft.AspNetCore.Mvc;

namespace IMS.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
