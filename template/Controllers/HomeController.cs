using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
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
			string username = ul.username;
			string password = ul.password;
			string email = ul.email;
			DataSet ds = ul.UserLoginData(password,email);
			ViewBag.data = ds.Tables[0];
			foreach (System.Data.DataRow dr in ViewBag.data.Rows)
			{
				TempData["UserLogin_id"] = dr["id"].ToString();

				return RedirectToAction("Index2");
			}
			return RedirectToAction("Login");
		}
		public IActionResult Index2()
		{
			return View();
		}
		public IActionResult WishList()
		{
			return View();
		}
		public IActionResult Books_Detail()
		{
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
		public IActionResult BooksGridView()
		{
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