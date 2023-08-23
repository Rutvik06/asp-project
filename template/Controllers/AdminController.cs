using Microsoft.AspNetCore.Http;
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
        public IActionResult AddBooks(AddBook ab,int a=0) {
          
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> AddBooks(AddBook ab,IFormFile formFile) 
        {
            var image = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "NewBooks", formFile.FileName);

            using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            string serializableString = image.ToString();
            TempData["image_name"] = serializableString;

            //TempData["image_name"] = image;
            ab.BookImage = image.ToString();
            ab.AddNewBook(ab.BookName, ab.BookCategory,ab.BookPrice,ab.BookDescription,ab.BookAuthor,ab.BookImage);
            return RedirectToAction("AddBooks");
        }
        [HttpGet]
        public IActionResult ViewBooks(ViewBooks vb)

        {
            DataSet ds = vb.selectNewBook();
            ViewBag.user_data = ds.Tables[0];

            //ViewBag.image = TempData["image_name"];
            //ViewBag.ImageUrl = Url.Content("~/image/" + TempData["image_name"]);
            List<string> imageUrls = new List<string>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                imageUrls.Add(Url.Content("~/NewBooks/" + dr["BookImage"].ToString()));
            }

            ViewBag.ImageUrls = imageUrls;
            return View();
        }
        public IActionResult deleteBookData(ViewBooks vb, int id)
        {
            vb.deleteBook(id);
            return RedirectToAction("ViewBooks");
            // GET
        }
        [HttpGet]
        public IActionResult UpdateBookData() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBookData(ViewBooks vb, IFormFile formFile,int a=0)
        {
            var image = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "NewBooks", formFile.FileName);

            using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            string serializableString = image.ToString();
            TempData["image_name"] = serializableString;

            //TempData["image_name"] = image;
            vb.BookImage = image.ToString();
            vb.updateBook(vb.id,vb.BookName, vb.BookCategory, vb.BookPrice, vb.BookDescription, vb.BookAuthor, vb.BookImage);
            return RedirectToAction("UpdateBookData");
        }
        public IActionResult Logout()
        {
            TempData.Clear();
            return RedirectToAction("Index");
        }
	}
}
