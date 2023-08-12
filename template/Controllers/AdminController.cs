using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Data;
using template.Models;

namespace template.Controllers
{
	public class AdminController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
            if (TempData.Peek("Login_id") != null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
		}
		[HttpPost]
        public IActionResult Index(AdminLogin db)
        {
			string email = db.email;
			string password = db.password;

            DataSet ds = db.AdminLoginData(email, password);
            ViewBag.data = ds.Tables[0];
			foreach(System.Data.DataRow dr in ViewBag.data.Rows) {
                TempData["Login_id"] = dr["id"].ToString();

                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("Index"); 
        }
        [HttpGet]
        public IActionResult Dashboard()
		{
            if (TempData.Peek("Login_id") == null)
            {
                return RedirectToAction("Index");
            }
            return View();
		}
        [HttpGet]
        public IActionResult AddBooks() {
            if (TempData["image_name"] == null)
            {
                // Set a default value for 'image' or handle the case where it's not available
                ViewBag.image = "default_image.jpg";
            }
            else
            {
                ViewBag.image = TempData["image_name"];
            }
            return View();
            
        }
        [HttpPost]
        public async Task<IActionResult> AddBooks(AddBook ab,IFormFile formFile) 
        {
            if (ab == null)
            {
                // Handle the null object case (e.g., return an error message or redirect)
                return BadRequest("Invalid input.");
            }

            var image = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim();
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AdminImage", uniqueFileName);
            using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            TempData["image_name"] = uniqueFileName;

            string BookName = ab.BookName;
            string BookCategory = ab.BookCategory;
            string BookAuthor = ab.BookAuthor;
            string BookDescription = ab.BookDescription;
            //string BookImage = ab.BookImage;
            string BookPrice = ab.BookPrice;
            string truncatedImage = ab.BookImage.Length > 50 ? ab.BookImage.Substring(0, 50) : ab.BookImage;
            ab.BookImage = $"~/AdminImage/{uniqueFileName}";
            ab.AddNewBook(BookName, BookCategory, BookAuthor, BookDescription, BookPrice,truncatedImage);

            TempData["image_name"] = image; 
            return RedirectToAction("AddBooks");
        }
        public IActionResult ViewBooks()
        {
            return View();
        }
        public IActionResult Logout()
        {
            TempData.Clear();
            return RedirectToAction("Index");
        }
	}
}
