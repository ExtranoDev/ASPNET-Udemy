using Microsoft.AspNetCore.Mvc;

namespace CRUDDemo.Controllers
{
    public class PersonsController : Controller
    {
        [Route("persons/index")]
        [Route("/")] 
        public IActionResult Index()
        {
            return View();
        }
    }
}
