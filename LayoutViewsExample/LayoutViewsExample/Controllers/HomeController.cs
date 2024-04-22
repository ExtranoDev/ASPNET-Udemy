using LayoutViewsExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace LayoutViewsExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("about")]
        public IActionResult About()
        {
            return View();
        }

        [Route("contact")]
        public IActionResult Contact()
        {
            return View();
        }
        [Route("programming-languages")]
        public IActionResult ProgrammingLanguages()
        {
            ListModel model = new()
            {
                ListTitle = "Programming Languages List",
                ListItems =
                [
                    "Python",
                    "C#",
                    "Go"
                ]
            };

            return PartialView("_ListPartialView", model);
        }

        [Route("friends-list")]
        public IActionResult LoadFriendsList() {
            PersonGridModel personGridModel = new PersonGridModel()
            {
                GridTitle = "Friends",
                Persons = new List<Person>() {
                    new Person() { PersonName = "Mia", JobTitle = "Developer" },
                    new Person() { PersonName = "Emma", JobTitle = "UI Designer" },
                    new Person() { PersonName = "Avva", JobTitle = "QA" }
                }
            };

            return ViewComponent("Grid", new {grid = personGridModel});
        }
    }
}
