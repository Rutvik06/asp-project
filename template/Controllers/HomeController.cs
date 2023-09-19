using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
		[HttpGet]
		public IActionResult AddWishList()
		{
			return View();
		}
		[HttpPost]
		public IActionResult AddWishList(WishList wl, ViewUserBooks vub, int id)
		{
			DataSet ds = vub.selectUserSideBookSingleBook(id);
			if (ds.Tables[0].Rows.Count > 0)
			{
				DataRow bookRow = ds.Tables[0].Rows[0];
				string bookName = bookRow["BookName"].ToString();
				string bookPrice = bookRow["BookPrice"].ToString();
				//string bookImg = bookRow["BookImage"].ToString();
				//string bookImg=TempData["BookImage"] as string;
				string bookImg = ViewBag.ImageUrls[0]; // Corrected this line to get the image URL from ViewBag
				wl.AddToWishList(bookName, bookPrice, bookImg);

				return RedirectToAction("Books_Detail");
			}
			else
			{
				return RedirectToAction("Books_Detail");
			}

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
		public IActionResult Shop_Cart(AddtoCart atc)
		{
			if (TempData.Peek("UserLogin_id") != null)
			{
				DataSet ds = atc.selectWithUserId();
				ViewBag.Cart_Data = ds.Tables[0];
				List<string> imageUrls = new List<string>();
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					imageUrls.Add(Url.Content("~/NewBooks/" + dr["BookImg"].ToString()));
				}

				ViewBag.ImageUrls = imageUrls;
				decimal orderSubtotal = 0; // Initialize order subtotal
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					// Calculate the subtotal for each item and add it to the order subtotal
					decimal itemSubtotal = Convert.ToDecimal(dr["BookPrice"]) * Convert.ToInt32(dr["BookQuantity"]);
					orderSubtotal += itemSubtotal;
				}

				// Calculate shipping charge and total
				decimal shippingCharge = 50; // You can calculate the shipping charge as needed
				decimal overallTotal = orderSubtotal + shippingCharge;

				ViewBag.OrderSubtotal = orderSubtotal;
				ViewBag.ShippingCharge = shippingCharge;
				ViewBag.OverallTotal = overallTotal;
			}
			else
			{
				return RedirectToAction("Login");
			}
			return View();
		}
		public IActionResult deleteCartData(AddtoCart atc,int id)
		{
			atc.deleteCart(id);
			return RedirectToAction("Shop_Cart");
		}
		//----------------------------------------------add new acccount in checkout and userlist
		[HttpPost]
		public IActionResult Shop_Checkout(UserAccount ua)
		{
			
			ua.AddAccount(ua.UserFName,ua.UserLName,ua.UserCountry,ua.UserEmail,ua.UserPhone,ua.UserAddress,ua.UserCity,ua.UserZip);
			return RedirectToAction("OrderPage");
		}
		public IActionResult Shop_Checkout()
		{
			return View();
		}
		//-------------------------------------------------my profile post
		[HttpPost]
		public IActionResult My_Profile(Add_Profile ap)
		{
			
			ap.AddNewProfile(ap.name, ap.profession, ap.language, ap.age, ap.contact, ap.email, ap.country, ap.pincode, ap.address, ap.city);
			return RedirectToAction("My_Profile");
		}
		//--------------------------------------------------my profile get
		[HttpGet]
		public IActionResult My_Profile(Add_Profile ap,int id)
		{
			DataSet ds = ap.selectUpdateProfile(id);
			//ViewBag.ProfileId = ;
			return View();
		}
		//----------------------------------------------------update profile post
		[HttpPost]
		public IActionResult Update_Profile(Add_Profile ap)
		{
			
			ap.AddNewProfile(ap.name, ap.profession, ap.language, ap.age, ap.contact, ap.email, ap.country, ap.pincode, ap.address, ap.city);
			return RedirectToAction("My_Profile");
		}
		//--------------------------------------------------update profile get 
		[HttpGet]
		public IActionResult Update_Profile(int a = 0)
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
				DataSet bookData = vb.selectSinNewBook(id);

				if (bookData.Tables[0].Rows.Count > 0)
				{
					DataRow bookRow = bookData.Tables[0].Rows[0];
					string bookName = bookRow["BookName"].ToString();
					string bookPrice = bookRow["BookPrice"].ToString();
					string bookQuantity = "1";
					string bookImg = bookRow["BookImage"].ToString();
					string userId = TempData.Peek("UserLogin_id").ToString();

					AddtoCart atc = new AddtoCart();

					// Check if the book already exists in the cart for the user
					DataSet cartItems = atc.GetCartItems(userId, bookName);

					if (cartItems.Tables[0].Rows.Count > 0)
					{
						// Book already exists in the cart, increase quantity
						DataRow cartItemRow = cartItems.Tables[0].Rows[0];
						int currentQuantity = Convert.ToInt32(cartItemRow["BookQuantity"]);
						int newQuantity = currentQuantity + 1;
						// Update the quantity in the cart
						atc.UpdateCartItemQuantity(userId, bookName, newQuantity);
					}
					else
					{
						// Book does not exist in the cart, add it
						atc.AddtoCartData(userId, bookName, bookPrice, bookQuantity, bookImg);
					}

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

		[HttpGet]
		public IActionResult OrderPage(AddtoCart atc)
		{
			DataSet ds = atc.selectWithUserId();
			ViewBag.OrderData = ds.Tables[0];
			List<string> imageUrls = new List<string>();
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				imageUrls.Add(Url.Content("~/NewBooks/" + dr["BookImg"].ToString()));
			}

			ViewBag.ImageUrls = imageUrls;
			return View();
		}
		[HttpPost]
		public IActionResult OrderPage(CardDetails cd)
		{
			cd.AddCardDetails(cd.CardName, cd.CardNum, cd.CardVerifyNum);
			return RedirectToAction("ViewOrders");
		}
		[HttpGet]
		public IActionResult OrderHistory(AddtoCart atc)
		{
			if (TempData.Peek("UserLogin_id") != null)
			{
				DataSet ds = atc.selectWithUserId();
				ViewBag.OrderData = ds.Tables[0];
				List<string> imageUrls = new List<string>();
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					imageUrls.Add(Url.Content("~/NewBooks/" + dr["BookImg"].ToString()));
				}

				ViewBag.ImageUrls = imageUrls;
				return View();
			}
			else
			{
				return RedirectToAction("Login");
			}
		}
		[HttpGet]
		public IActionResult ViewOrders(AddtoCart atc)
		{
			DataSet ds = atc.selectWithUserId();
			ViewBag.OrderData = ds.Tables[0];
			List<string> imageUrls = new List<string>();
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				imageUrls.Add(Url.Content("~/NewBooks/" + dr["BookImg"].ToString()));
			}

			ViewBag.ImageUrls = imageUrls;
			return View();
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