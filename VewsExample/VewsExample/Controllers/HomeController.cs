using Microsoft.AspNetCore.Mvc;

namespace VewsExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("home")]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
