using Microsoft.AspNetCore.Mvc;
using LayoutViewsExample.Models;

namespace LayoutViewsExample.ViewComponents
{
    public class GridViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            PersonGridModel model = new() { 
                GridTitle = "Persons List",
                Persons =
                [
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
                ]
            };
            ViewData["Grid"] = model;
            return View("Default", model);
        }
    }
}
