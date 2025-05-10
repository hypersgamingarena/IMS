using Microsoft.AspNetCore.Mvc;

namespace IMS.Controllers
{
    public class InventoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
