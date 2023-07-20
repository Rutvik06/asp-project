using Microsoft.AspNetCore.Mvc;
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
        public IActionResult AddBooks()
        {
            return View();
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
