using Microsoft.AspNetCore.Mvc;
using Models;

namespace WeatherAppProject.ViewComponents
{
    public class CityViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(CityWeather city)

        {
            ViewBag.CityCssClass = GetStyleByFahrenheit(city.TemperatureFahrenheit);
            return View(city);
        }

        private string GetStyleByFahrenheit(int temperatureFahrenheit)
        {
            return temperatureFahrenheit switch
            {
                (< 44) => "blue-back",
                (>= 44) and (< 75) => "green-back",
                (>= 75) => "orange-back"
            };
        }
    }
}
