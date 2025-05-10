using Microsoft.AspNetCore.Mvc;

namespace IMS.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
