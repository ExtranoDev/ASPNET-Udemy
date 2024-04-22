using Microsoft.AspNetCore.Mvc;
using LayoutViewsExample.Models;

namespace LayoutViewsExample.ViewComponents
{
    public class GridViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(PersonGridModel grid)
        {            
            return View("Default", grid);
        }
    }
}
