using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ControllerBankProject.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return Content("<h1>Welcome to the Best Bank</h1>", "text/html");
        }

        [Route("account-details")]
        public JsonResult AccountDetails()
        {
            var details = new
            {
                accountNumber = 1001,
                accountHolderName = "Example Nam",
                currentBalance = 500
            };
            
            return Json(details);
        }

        [Route("account-statement")]
        public VirtualFileResult AccountStatement()
        {
            return File("/MyDoc07_33_26.pdf", "application/pdf");
        }

        [Route("get-current-balance/{accountNumber:int?}")]
        public IActionResult AccountBalance()
        {           
            int accountNumber = Convert.ToInt32(Request.RouteValues["accountNumber"]);
            
            // checks if routeValue accountNumber is missing or equals to zero
            if (accountNumber == 0)
                return BadRequest("Account Number should be supplied");
            
            // checks if routeValue accountNumber is no equals to 1001
            if (accountNumber != 1001)
                return NotFound("Account Number should be 1001");
            
            // Hard coded will be edited for future purpose
            var details = new
            {
                accountNumber = 1001,
                accountHolderName = "Example Nam",
                currentBalance = 500
            };
            return Content(details.currentBalance.ToString(), "text/plain");
        }
    }
}
