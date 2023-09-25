using ECommerceFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerceFirst.Controllers
{
    public class HomeController : Controller
    {
        [Route("order")]
        public IActionResult Index(Order order)
        {
            if (!ModelState.IsValid)
            {
                string errors = string.Join("\n",
                    ModelState.Values.SelectMany(value => value.Errors)
                    .Select(err => err.ErrorMessage).ToList());

                return BadRequest(errors);
            }

            Random rand = new Random();
            order.OrderNo = rand.Next(1, 2000);

            OrderReturn result = new OrderReturn() { 
                OrderNo = (int)order.OrderNo
            };

            return Json(result);
        }
    }
}
