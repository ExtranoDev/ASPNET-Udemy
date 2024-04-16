using Microsoft.AspNetCore.Mvc;
using LayoutViewsExample.Models;

namespace LayoutViewsExample.ViewComponents
{
    public class GridViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            PersonGridModel model = new PersonGridModel() { 
                GridTitle = "Persons List",
                Persons = new List<Person>()
                {
                    new Person()
                    {
                        PersonName = "John",
                        JobTitle = "Sax Player",
                    },
                    new Person()
                    {
                        PersonName = "Jones",
                        JobTitle = "Manager",
                    },
                    new Person()
                    {
                        PersonName = "Andrew",
                        JobTitle = "Front-end Developer",
                    }
                }
            };
            ViewData["Grid"] = model;
            return View(); // invoked a partial view Viewa/Shared/Components/Grid/Default.cshtml
        }
    }
}
