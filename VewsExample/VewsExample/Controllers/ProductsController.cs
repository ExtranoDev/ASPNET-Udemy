using Microsoft.AspNetCore.Mvc;

namespace VewsExample.Controllers
{
    public class ProductsController : Controller
    {
        [Route("products/all")]
        public IActionResult all()
        {
            return View();
        }
    }
}
