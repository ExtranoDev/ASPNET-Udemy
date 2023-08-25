using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers
{
    public class StoreController : Controller
    {
        [Route("store/books/{id}")]
        public IActionResult Books([FromRoute]bool isloggedIn)
        {
            int id = Convert.ToInt32(Request.RouteValues["id"]);
            return Content($"<h3>Book Store {id}</h3><br><h3>User Logged In: {isloggedIn}</h3>", "text/html");
        }
    }
}
