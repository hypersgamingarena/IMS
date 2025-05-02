using Microsoft.AspNetCore.Mvc;

namespace IMS.Controllers
{
    public class WarehouseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
