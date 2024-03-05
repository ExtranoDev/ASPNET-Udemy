using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using VewsExample.Models;

namespace VewsExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("home")]
        [Route("/")]
        public IActionResult Index()
        {
            ViewData["appTitle"] = "View Project";
            List<Person> people = new List<Person>()
            {
                new Person()
                {
                    Name = "John",
                    DateOfBirth = DateTime.Parse("2000-05-06"),
                    PersonGender = Gender.Male
                },
                new Person()
                {
                    Name = "Linda",
                    DateOfBirth = DateTime.Parse("2005-01-09"),
                    PersonGender = Gender.Female
                },
                new Person()
                {
                    Name = "Susan",
                    DateOfBirth = DateTime.Parse("2008-07-17"),
                    PersonGender = Gender.Other
                }
            };
            ViewData["people"] = people;
            return View(people);
        }

        [Route("person-details/{name}")]
        public IActionResult Details(string? name)
        {
            if (name == null)
                return Content("Name doesn't exist");
            List<Person> people = new List<Person>()
            {
                new Person()
                {
                    Name = "John",
                    DateOfBirth = DateTime.Parse("2000-05-06"),
                    PersonGender = Gender.Male
                },
                new Person()
                {
                    Name = "Linda",
                    DateOfBirth = DateTime.Parse("2005-01-09"),
                    PersonGender = Gender.Female
                },
                new Person()
                {
                    Name = "Susan",
                    DateOfBirth = DateTime.Parse("2008-07-17"),
                    PersonGender = Gender.Other
                }
            };
            Person? matchingPerson = people.Where(x => x.Name == name).FirstOrDefault();

            return View(matchingPerson);
        }

        [Route("person-with-product")]
        public IActionResult PersonWithProduct()
        {
            Person person = new()
            {
                Name = "Linda",
                DateOfBirth = DateTime.Parse("2005-01-09"),
                PersonGender = Gender.Female
            };

            Product product = new()
            {
                ProductId = 1,
                ProductName = "Brazillian Human Hair"
            };

            PersonAndProductWrapperModel personAndProductWrapperModel = new()
            {
                PersonData = person,
                ProductData = product
            };
            return View(personAndProductWrapperModel);
        }

        [Route("home/all-products")]
        public IActionResult all()
        {
            return View();
        }

        List<CityWeather> cities = new() {
            new() {
                CityUniqueCode = "LDN",
                CityName = "London",
                DateAndTime = Convert.ToDateTime("2030-01-01 8:00"),
                TemperatureFahrenheit = 33
            },
            new() {
                CityUniqueCode = "NYC",
                CityName = "London",
                DateAndTime = Convert.ToDateTime("2030-01-01 3:00"),
                TemperatureFahrenheit = 60
            },
            new() {
                CityUniqueCode = "PAR",
                CityName = "Paris",
                DateAndTime = Convert.ToDateTime("2030-01-01 9:00"),
                TemperatureFahrenheit = 82
            }
        };


        [Route("weather")]
        public IActionResult CityAll() {
            return View(cities);
        }
        
        [Route("/weather/{cityCode}")]
        public IActionResult City(string cityCode)
        {
            CityWeather? cityInfo = cities.FirstOrDefault(x => x.CityUniqueCode == cityCode);
            if (cityInfo != null)
            {
                return View(cityInfo);
            }
            return View("cityAll");
        }
    }
}
