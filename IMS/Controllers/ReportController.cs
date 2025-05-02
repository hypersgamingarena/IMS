using Microsoft.AspNetCore.Mvc;

namespace IMS.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
