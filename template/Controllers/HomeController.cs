using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Net;
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
				return RedirectToAction("Index");
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

		//---------------------------------------single user side book view
		[HttpGet]
		public IActionResult Books_Detail(ViewUserBooks vub,string id,int a=0)
		{
			if(int.TryParse(id,out int bookId))
			{

			DataSet ds = vub.selectUserSideBookSingleBook(bookId) ;

			DataRow bookDataRow = ds.Tables[0].Rows[0]; // Assuming there's only one row for the selected book

			List<string> imageUrls = new List<string>();
			imageUrls.Add(Url.Content("~/NewBooks/" + bookDataRow["BookImage"].ToString()));

			ViewBag.BookData = bookDataRow;
			ViewBag.ImageUrls = imageUrls;
			//vub.selectUserSideBookSingleBook(id);
			}
			return View();
		}
		public IActionResult Shop_Cart()
		{
			return View();
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
		public IActionResult BooksGridView(ViewUserBooks vub)
		{
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
		public IActionResult BlogDetail()
		{
			return View();
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