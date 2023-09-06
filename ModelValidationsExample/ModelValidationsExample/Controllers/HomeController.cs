using Microsoft.AspNetCore.Mvc;
using ModelValidationsExample.CustomModelBinders;
using ModelValidationsExample.Models;

namespace ModelValidationsExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("register")]
        //[Bind(nameof(Person.PersonName), nameof(Person.Email),
        //nameof(Person.Password), nameof(Person.ConfirmPassword))]
        //[ModelBinder(BinderType = typeof(PersonModelBinder))] [FromBody] 
        public IActionResult Index(Person person, [FromHeader(Name = "User-Agent")] string UserAgent)
        {
            if (!ModelState.IsValid)
            {
                string errors = string.Join("\n",
                    ModelState.Values.SelectMany(value => value.Errors).
                    Select(err => err.ErrorMessage).ToList());

                return BadRequest(errors);
            }
            
            return Content($"{person}, { UserAgent}");
        }
    }
}
