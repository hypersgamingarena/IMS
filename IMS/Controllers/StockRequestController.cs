using Microsoft.AspNetCore.Mvc;

namespace IMS.Controllers
{
    public class StockRequestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
