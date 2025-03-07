using Microsoft.AspNetCore.Mvc;
using Models;
using ServiceContracts;

namespace WeatherAppProject.Controllers
{
    public class WeatherController : Controller
    {
        // private field to hold service instance reference
        private readonly IWeatherService _weatherService;

        // constructor and IWeatherService injection
        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            var cities = _weatherService.GetCityWeatherDetails();
            return View(cities);
        }

        [Route("weather/{cityCode?}")]
        public IActionResult Weather(string? cityCode)
        {
            if (string.IsNullOrEmpty(cityCode))
            {
                return View();
            }

            CityWeather? city = _weatherService.GetWeatherByCityCode(cityCode);

            return View(city);
        }
    }
}
