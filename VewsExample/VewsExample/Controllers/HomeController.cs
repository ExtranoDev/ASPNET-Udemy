﻿using Microsoft.AspNetCore.Mvc;
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
    }
}
