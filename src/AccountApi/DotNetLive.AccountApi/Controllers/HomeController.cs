using Microsoft.AspNetCore.Mvc;

namespace DotNetLive.AccountApi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //return Redirect("~/swagger");
            return View();
        }
    }
}
