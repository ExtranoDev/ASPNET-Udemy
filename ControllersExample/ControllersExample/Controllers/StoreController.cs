using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers
{
    public class StoreController : Controller
    {
        [Route("store/books")]
        public IActionResult Books()
        {
            return View();
        }
    }
}
