using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StockAppProject.Options;

namespace StockAppProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<SocialMediaLinksOptions> _socialMediaLinksOptions;

        public HomeController(IOptions<SocialMediaLinksOptions> socialMediaLinksOptions)
        {
            _socialMediaLinksOptions = socialMediaLinksOptions;
        }

        [Route("/")]
        public IActionResult Index()
        {            
            ViewBag.facebook = _socialMediaLinksOptions.Value.Facebook;
            ViewBag.twitter = _socialMediaLinksOptions.Value.Twitter;
            ViewBag.youtube = _socialMediaLinksOptions.Value.Youtube;
            ViewBag.instagram = _socialMediaLinksOptions.Value.Instragram;
            return View();
        }
    }
}
