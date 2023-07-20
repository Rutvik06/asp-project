using Microsoft.AspNetCore.Mvc;
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
		public IActionResult Login()
		{
			return View();
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
			return RedirectToAction("Register");

           

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