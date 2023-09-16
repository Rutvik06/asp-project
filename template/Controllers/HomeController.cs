using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using template.Models;

namespace template.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
			
            return View();
        }
		[HttpGet]
		public IActionResult Login()
		{
			if (TempData.Peek("UserLogin_id") != null)
			{
				return RedirectToAction("BooksGridView");
			}
			return View();
		}
		[HttpPost]
		public IActionResult Login(UserLogin ul)
		{
			string email = ul.email;
			string password = ul.password;
			DataSet ds = ul.UserLoginData(email, password);
			ViewBag.data = ds.Tables[0];
			foreach (System.Data.DataRow dr in ViewBag.data.Rows)
			{
				TempData["UserLogin_id"] = dr["id"].ToString();
				return RedirectToAction("Index");
			}
			return RedirectToAction("Login"); // Change this line to RedirectToAction("Index");
		}

		public IActionResult Index2()
		{
			return View();
		}
		public IActionResult WishList()
		{
			return View();
		}

	//------------------------------------------------------user side single book
		[HttpGet]
		public IActionResult Books_Detail(ViewUserBooks vub, int id, int a = 0)
		{
			DataSet ds = vub.selectUserSideBookSingleBook(id);

			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				DataRow bookDataRow = ds.Tables[0].Rows[0];

				List<string> imageUrls = new List<string>();
				imageUrls.Add(Url.Content("~/NewBooks/" + bookDataRow["BookImage"].ToString()));

				ViewBag.BookData = bookDataRow;
				ViewBag.ImageUrls = imageUrls;

			}
			
				return View();
		}
		//------------------------------------------------- cart item view
		[HttpGet]
		public IActionResult Shop_Cart(AddtoCart atc,int UserId)
		{
			DataSet ds = atc.selectWithUserId(UserId);
			ViewBag.Cart_Data = ds.Tables[0];
			List<string> imageUrls = new List<string>();
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				imageUrls.Add(Url.Content("~/NewBooks/" + dr["BookImg"].ToString()));
			}

			ViewBag.ImageUrls = imageUrls;

			return View();
		}

		//----------------------------------------------add new acccount in checkout and userlist
		[HttpPost]
		public IActionResult Shop_Checkout(UserAccount ua)
		{
			
			ua.AddAccount(ua.UserFName,ua.UserLName,ua.UserCountry,ua.UserEmail,ua.UserPhone,ua.UserAddress,ua.UserCity,ua.UserZip);
			return RedirectToAction("Shop_Checkout");
		}
		public IActionResult Shop_Checkout()
		{
			return View();
		}
		public IActionResult My_Profile()
		{
			return View();
		}
		public IActionResult Contact_Us()
		{
			return View();
		}
		public IActionResult About_Us()
		{
			return View();
		}
		public IActionResult Services()
		{
			return View();
		}
		public IActionResult FAQs()
		{
			return View();
		}
		public IActionResult Help_Desk()
		{
			return View();
		}
		public IActionResult Comming_Soon()
		{
			return View();
		}
		[HttpGet]
		public IActionResult Register() {
			return View();
		}
		[HttpPost]
		public IActionResult Register(UserLogin ul)
		{
			string username = ul.username;
			string email = ul.email;
			string password = ul.password;
			ul.UserRegister(username,email, password);
			return RedirectToAction("Login");

           

		}

		//----------------------------------------books grid view for purchase

		[HttpGet]
		public IActionResult BooksGridView(ViewUserBooks vub,AddCategory Aac)
		{

			DataSet categoryData = Aac.selectNewCategory();
			ViewBag.CategoryData = categoryData;
			DataSet ds = vub.selectUserSideBooks();
            ViewBag.user_data = ds.Tables[0];
            List<string> imageUrls = new List<string>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                imageUrls.Add(Url.Content("~/NewBooks/" + dr["BookImage"].ToString()));
            }
            ViewBag.ImageUrls = imageUrls;
			

			return View();
		}
		//----------------------------------------------------add to cart function

		[HttpPost]
		public IActionResult AddToCart(int id)
		{
			if (TempData.Peek("UserLogin_id") != null)
			{
				ViewBooks vb = new ViewBooks();
				DataSet bookData = vb.selectSinNewBook(id); // Retrieve book data using the id from the Add_Book table

				if (bookData.Tables[0].Rows.Count > 0)
				{
					DataRow bookRow = bookData.Tables[0].Rows[0];
					string bookName = bookRow["BookName"].ToString();
					string bookPrice = bookRow["BookPrice"].ToString();
					string bookQuantity = "1";
					string bookImg = bookRow["BookImage"].ToString();
					AddtoCart atc = new AddtoCart();
					atc.AddtoCartData(bookName, bookPrice, bookQuantity, bookImg);

					return RedirectToAction("BooksGridView");
					
				}
				else
				{
					
					return RedirectToAction("BooksGridView"); 
				}
			}
			else
			{
				return RedirectToAction("Login");
			}
		}



		public IActionResult BlogDetail()
		{
			return View();
		}
		public IActionResult LogOut()
		{
			TempData.Clear();
			return RedirectToAction("Login");
		}
		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}