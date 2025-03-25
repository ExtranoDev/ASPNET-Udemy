using Autofac;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Services;

namespace DIExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICitiesService _citiesService1;
        private readonly ICitiesService _citiesService2;
        private readonly ICitiesService _citiesService3;
        private readonly ILifetimeScope _lifeTimeScope;
        private IWebHostEnvironment _webHostEnvironment;

        //constructor
        public HomeController(
            ICitiesService citiesService1,
            ICitiesService citiesService2,
            ICitiesService citiesService3,
            ILifetimeScope serviceScopeFactory,
            IWebHostEnvironment webHostEnvironment)
        {
            _citiesService1 = citiesService1;
            _citiesService2 = citiesService2;
            _citiesService3 = citiesService3;
            _lifeTimeScope = serviceScopeFactory;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("/")]
        public IActionResult Index()
        {
            List<string> cities = _citiesService1.GetCities();
            ViewBag.InstanceID_CitiesService1 = _citiesService1.ServiceInstanceId;
            ViewBag.InstanceID_CitiesService2 = _citiesService2.ServiceInstanceId;
            ViewBag.InstanceID_CitiesService3 = _citiesService3.ServiceInstanceId;

            using (ILifetimeScope scope = _lifeTimeScope.BeginLifetimeScope())
            {
                //Inject CitiesService
                ICitiesService citiesService = 
                scope.Resolve<ICitiesService>();
                //DB Work
                ViewBag.InstanceID_CitiesService_InScope = citiesService.ServiceInstanceId;
            }//end of scope;; calls Dispose method automatically here

            return View(cities);
        }
    }
}
