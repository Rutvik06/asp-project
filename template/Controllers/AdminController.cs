using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Data;
using template.Models;

namespace template.Controllers
{
	public class AdminController : Controller
	{
        //---------------------------Index get 
		
        [HttpGet]
		public IActionResult Index()
		{
            if (TempData.Peek("Login_id") != null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
		}
        //-------------------------Index post

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

        //------------------------------------dashboard get
        
        [HttpGet]
        public IActionResult Dashboard()
		{
            if (TempData.Peek("Login_id") == null)
            {
                return RedirectToAction("Index");
            }
            return View();
		}

        //------------------------------------------- addbooks get

        [HttpGet]
        public IActionResult AddBooks(AddBook ab,int a=0) {
           
            AddCategory addCategory = new AddCategory();
            DataSet categoryData = addCategory.selectNewCategory();
            ViewBag.CategoryData = categoryData;
            AddAuthor addAuthor = new AddAuthor();
            DataSet authorData = addAuthor.selectNewAuthor();
            ViewBag.AuthorData = authorData;
            
            return View();
        }

        //-------------------------------------------addbooks post
        
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

        //--------------------------------------------- view books get
        
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

        //----------------------------------------------delete books

        public IActionResult deleteBookData(ViewBooks vb, int id)
        {
            vb.deleteBook(id);
            return RedirectToAction("ViewBooks");
            // GET
        }

        //---------------------------------------------- update books get

        [HttpGet]
        public IActionResult UpdateBookData(int id)
        {
            ViewBooks vb = new ViewBooks(); // Create an instance of ViewBooks
            DataSet bookData = vb.selectUpdateBook(id); // Retrieve book data using the id
            AddCategory addCategory = new AddCategory();
            DataSet categoryData = addCategory.selectNewCategory();
            ViewBag.CategoryData = categoryData;
            AddAuthor addAuthor = new AddAuthor();
            DataSet authorData = addAuthor.selectNewAuthor();
            ViewBag.AuthorData = authorData;
            return View(bookData); // Pass the book data to the view
        }
        //------------------------------------------------update books post

        [HttpPost]
		public async Task<IActionResult> UpdateBookData(ViewBooks vb, IFormFile formFile, int a = 0)
		{
			// Check if an image file was uploaded
			if (formFile != null)
			{
				var image = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim();
				var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "NewBooks", formFile.FileName);

				using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
				{
					await formFile.CopyToAsync(stream);
				}

				vb.BookImage = image.ToString();
			}

			// Update book data using vb object
			vb.updateBook(vb.id, vb.BookName, vb.BookCategory, vb.BookPrice, vb.BookDescription, vb.BookAuthor, vb.BookImage);

			return RedirectToAction("UpdateBookData");
		}


		//------------------------------------------------- category get

		[HttpGet]
        public IActionResult ViewCategory(AddCategory ac,int a = 0)
        {
            DataSet ds = ac.selectNewCategory();
            ViewBag.category_data = ds.Tables[0];
            return View();
        }

        //------------------------------------------------- category post
        [HttpPost]
        public IActionResult AddCategory(AddCategory ac)
        {
            string category = ac.category;
            string description = ac.description;
            ac.AddNewCategory(category,description);
            return RedirectToAction("AddCategory");
        }

        //----------------------------------------------delete new category

        public IActionResult deleteNewCategory(AddCategory ac, int id)
        {
            ac.deleteNewCategory(id);
            return RedirectToAction("ViewCategory");
            // GET
        }
        //----------------------------------------------update category post
        [HttpPost]
        public IActionResult UpdateCategory(AddCategory ac)
        {

            string category = ac.category;
            string description = ac.description;

            ac.updateNewCategory(ac.id,category,description);
            return RedirectToAction("UpdateCategory");
        }
        //-----------------------------------------------update category get
        [HttpGet]
        public IActionResult UpdateCategory(AddCategory ac,int id,int a=0)
        {
			
			DataSet categoryData = ac.selectSinCategory(id); 
			
			return View(categoryData);
        }
        //---------------------------------------------add category
        public IActionResult AddCategory()
        {
            return View();
        }

        //------------------------------------------------author post
        [HttpPost]
        public async Task<IActionResult> AddAuthor(AddAuthor Aab,IFormFile formFile)
        {
            var image = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "NewAuthor", formFile.FileName);

            using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            string serializableString = image.ToString();
            TempData["image_name"] = serializableString;

            //TempData["image_name"] = image;
            Aab.AuthorImg = image.ToString();
            Aab.AddNewBook(Aab.AuthorName, Aab.AuthorDescription, Aab.AuthorEmail, Aab.AuthorImg);          
            return RedirectToAction("AddAuthor");
        }
        //-------------------------------------------------- add author get

        public IActionResult AddAuthor()
        {
            return View();
        }
        //-------------------------------------------------------view new author
        [HttpGet]
        public IActionResult ViewAuthor(AddAuthor Aab)
        {
            DataSet ds = Aab.selectNewAuthor();
            ViewBag.author_data = ds.Tables[0];

            //ViewBag.image = TempData["image_name"];
            //ViewBag.ImageUrl = Url.Content("~/image/" + TempData["image_name"]);
            List<string> imageUrls = new List<string>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                imageUrls.Add(Url.Content("~/NewAuthor/" + dr["AuthorImg"].ToString()));
            }

            ViewBag.ImageUrls = imageUrls;
            return View();
        }

        //----------------------------------------------delete author


        public IActionResult deleteNewAuthor(AddAuthor Aab, int id)
        {
            Aab.deleteAuthor(id);
            return RedirectToAction("ViewAuthor");
            // GET
        }
        //------------------------------------------------author update post
        [HttpPost]
        public async Task<IActionResult> UpdateAuthor(AddAuthor Aab, IFormFile formFile)
        {
            var image = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "NewAuthor", formFile.FileName);

            using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            string serializableString = image.ToString();
            TempData["image_name"] = serializableString;

            //TempData["image_name"] = image;
            Aab.AuthorImg = image.ToString();
            Aab.updateAuthor(Aab.id,Aab.AuthorName, Aab.AuthorDescription, Aab.AuthorEmail, Aab.AuthorImg);
            return RedirectToAction("AddAuthor");
        }
        //-----------------------------------------------------author update get
        [HttpGet]
        public IActionResult UpdateAuthor(AddAuthor Aab,int id,int a=0)
        {
            DataSet authorData = Aab.selectSinAuthor(id);

            return View(authorData);
        }

        //---------------------------------------------------select User list
        public IActionResult UserList(UserList ul)
        {
            DataSet ds = ul.selectUserList();
            ViewBag.selectUser_data = ds.Tables[0];
            return View();
        }
        //-----------------------------------------------delete user
        public IActionResult deleteUser(UserList ul, int id)
        {
            ul.deleteUser(id);
            return RedirectToAction("UserList");
            // GET
        }

        //--------------------------------------------------get orders
        [HttpGet]
        public IActionResult ViewOrders(Order od)
        {
            
			DataSet ds = od.ViewAdminOrder();
			ViewBag.user_data = ds.Tables[0];

			//ViewBag.image = TempData["image_name"];
			//ViewBag.ImageUrl = Url.Content("~/image/" + TempData["image_name"]);
			List<string> imageUrls = new List<string>();
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				imageUrls.Add(Url.Content("~/NewBooks/" + dr["BookImg"].ToString()));
			}

			ViewBag.ImageUrls = imageUrls;
			return View();
        }

        //----------------------------------------------Update order status get
        [HttpGet]
        public IActionResult UpdateStatus(Order od,int id)
        {
            DataSet bookdata = od.UpdateOrderId(id);
            //ViewBag.user_data = ds.Tables[0];
            return View(bookdata);
        }
        //-----------------------------------------------update order status post
        [HttpPost]
        public IActionResult UpdateStatus(Order od)
        {
            od.updateStatus(od.id, od.OrderStatus);
            return RedirectToAction("UpdateStatus");
        }
        
        //----------------------------------------------logout
        public IActionResult Logout()
        {
            TempData.Clear();
            return RedirectToAction("Index");
        }
	}
}
