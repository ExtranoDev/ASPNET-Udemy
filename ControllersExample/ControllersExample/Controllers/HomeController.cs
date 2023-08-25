using ControllersExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("home")]
        [Route("/")]
        public ContentResult Index()
        {
            return Content("<h1>Welcome to my website</h1><br><p>This is an HTML paragraph tag</p>", "text/html");
            //return Content("Hello from Akure, Ondo, Nigeria.", "text/plain"); 
            
            //new ContentResult() { 
            //    Content = "Hello from Akure, Ondo, Nigeria.", 
            //    ContentType = "text/plain" 
            //};
        }
        
        [Route("person")]
        public JsonResult Person()
        {
            Person person = new Person() { 
                Id = Guid.NewGuid(),
                FirstName = "James",
                LastName = "Smith",
                Age = 25
            };
            return Json(person);
        }

        [Route("contact-us/{mobile:regex(^\\d{{10}}$)}")]
        public string Contact()
        {
            return "Hello contact us at +2348100423257.";
        }

        [Route("file-download")]
        public VirtualFileResult FileDownload()
        {
            return File("/jegs-sample.pdf", "application/pdf");
        }

        [Route("file-download2")]
        public PhysicalFileResult FileDownload2()
        {
            return PhysicalFile(@"C:\Users\joshua.oguntola\Desktop\MyDoc07_33_26.pdf", "application/pdf");
        }

        [Route("file-download3")]
        public FileContentResult FileDownload3()
        {
            byte[] bytes = System.IO.File.ReadAllBytes(@"C:\Users\joshua.oguntola\Desktop\MyDoc07_33_26.pdf");
            Console.WriteLine(bytes);
            return File(bytes, "application/pdf");
        }

        [Route("request-test/{mobile:regex(^\\d{{10}}$)?}")]
        public IActionResult RequestTest()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Request.Query["mobile"])))
            {
                //Response.StatusCode = 400;
                // return NotFound(); takes status code 400
                //return Unauthorized();
                // Same as return Unauthorized()
                return StatusCode(200);
            } else if (Convert.ToString(Request.Query["mobile"]).Count() < 10)
            {
                Response.StatusCode = 500;
                return BadRequest();
            }
            return Content("You will be fine", "text/plain");
        }

        [Route("bookstore/{bookID:int}/{isloggedin?}")]
        public IActionResult RedirectTest([FromRoute]int? bookID, [FromQuery]bool isloggedIn, Book book)
        {
            // return new RedirectToActionResult("Books", "Store", new { }); // 302 - Found temporary redirection
            //return new RedirectToActionResult("Books", "Store", new { }, permanent: true);// 301 moved permanently
            // return RedirectToActionPermanent("Books", "Store", new { id = bookID });
            if (bookID.HasValue == false)
                return BadRequest("Book id cannot be null or empty");

            return Content($"Book ID: {book.BookId}\nAuthor: {book.Author}\nLogged In: {isloggedIn}\n");
        }
    }
}
