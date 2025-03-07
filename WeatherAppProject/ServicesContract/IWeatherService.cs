
using Models;

namespace ServiceContracts
{
    public interface IWeatherService
    {
        List<CityWeather> GetCityWeatherDetails();

        CityWeather? GetWeatherByCityCode(string cityCode);
    }
}
