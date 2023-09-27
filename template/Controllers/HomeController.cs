using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
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
		[HttpGet]
        public IActionResult Index(ViewBooks vb)
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
				DataRow userRow = ds.Tables[0].Rows[0];
				TempData["UserLogin_id"] = userRow["id"].ToString();
				//TempData["UserEmail"] = userRow["email"].ToString();
				//TempData["UserName"] = userRow["name"].ToString();
				return RedirectToAction("Index");
			}
			return RedirectToAction("Login"); // Change this line to RedirectToAction("Index");
		}

		
		[HttpGet]
		public IActionResult WishList(WishList wl)
		{
            if (TempData.Peek("UserLogin_id") != null)
			{
                int userId = int.Parse((string)TempData.Peek("UserLogin_id"));
                DataSet ds = wl.SelectData(userId);
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
			else
			{
				return RedirectToAction("Login");
			}
                
		}
		[HttpGet]
		public IActionResult AddWishList()
		{
			return View();
		}
		[HttpPost]
		public IActionResult AddWishList(WishList wl, ViewUserBooks vub, int id)
		{
			if (TempData.Peek("UserLogin_id") != null)
			{
				DataSet ds = vub.selectUserSideBookSingleBook(id);
				if (ds.Tables[0].Rows.Count > 0)
				{
					DataRow bookRow = ds.Tables[0].Rows[0];
					string bookName = bookRow["BookName"].ToString();
					string bookPrice = bookRow["BookPrice"].ToString();
					string bookImg = bookRow["BookImage"].ToString();
					string userId = TempData.Peek("UserLogin_id").ToString();

					//string bookImg=TempData["BookImage"] as string;
					//string bookImg = ViewBag.ImageUrls[0]; // Corrected this line to get the image URL from ViewBag
					wl.AddToWishList(userId,bookName, bookPrice, bookImg);

					return RedirectToAction("BooksGridView");
				}
				else
				{
					return RedirectToAction("ErrorPage");
				}
			}
			else
			{
				return RedirectToAction("Login");
			}
			return View();
		}

		public IActionResult deleteWishList(WishList wl,int id)
		{
			wl.deleteBook(id);
			return RedirectToAction("WishList");
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
			ViewBag.user_data = ds.Tables[0];

			//ViewBag.image = TempData["image_name"];
			//ViewBag.ImageUrl = Url.Content("~/image/" + TempData["image_name"]);
			List<string> imageUrls2 = new List<string>();
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				imageUrls2.Add(Url.Content("~/NewBooks/" + dr["BookImage"].ToString()));
			}

			ViewBag.ImageUrls = imageUrls2;

			return View();
		}
		//------------------------------------------------- cart item view
		[HttpGet]
		public IActionResult Shop_Cart(AddtoCart atc)
		{
			if (TempData.Peek("UserLogin_id") != null)
			{
				int userId = int.Parse((string)TempData.Peek("UserLogin_id"));
				DataSet ds = atc.selectUserWithJoin(userId);
				if (ds.Tables[0].Rows.Count > 0) {
					ViewBag.Cart_Data = ds.Tables[0];
					//ViewBag.Cart_ID = ds;
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
				else { return RedirectToAction("EmptyCartPage"); }
					
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
		public async Task<IActionResult> My_Profile(Add_Profile ap, IFormFile formFile)
		{
			if (TempData.Peek("UserLogin_id") != null) {
				string userId = TempData.Peek("UserLogin_id").ToString();
				var image = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim();
				var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "NewBooks", formFile.FileName);

				using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
				{
					await formFile.CopyToAsync(stream);
				}
				string serializableString = image.ToString();
				TempData["image_name"] = serializableString;

				//TempData["image_name"] = image;
				ap.profileimg = image.ToString();
				ap.AddNewProfile(ap.name, ap.profession, ap.language, ap.age, ap.contact, ap.email, ap.country, ap.pincode, ap.address, ap.city,userId,ap.profileimg);
				return RedirectToAction("My_Profile");
			}
			else
			{
				return RedirectToAction("Login");
			}
		}
		//--------------------------------------------------my profile get
		[HttpGet]
		public IActionResult My_Profile(Add_Profile ap,int userId)
		{
			//DataSet ds = ap.selectUpdateProfile(userId);
			//ViewBag.ProfileId = ds.Tables[0];
			return View();
		}

		[HttpGet] 
		public IActionResult View_Profile(Add_Profile ap, int userId)
		{
			if (TempData.Peek("UserLogin_id") != null)
			{
				string LoginId = TempData.Peek("UserLogin_id").ToString();
				if (LoginId == userId.ToString())
				{

					DataSet ds = ap.selectUpdateProfile(userId);


					if (ds.Tables[0].Rows.Count > 0)
					{
						DataRow dr = ds.Tables[0].Rows[0];

						//ViewBag.UserName = dr["name"].ToString();
                        ViewBag.UserName = dr["userId"].ToString();


                        return View();
					}
					else
					{
						// Handle the case where the user doesn't have a profile yet
						return RedirectToAction("My_Profile");
					}
				}
				else
				{
					return RedirectToAction("My_Profile");
				}
			}
			else
			{
				return RedirectToAction("Login");
			}
			return View();
		}
		
		[HttpPost]
		public IActionResult Contact_Us(ContactUs cu)
		{
			cu.AddContact(cu.fullname, cu.email, cu.phone, cu.message);
			return RedirectToAction("Index");
		}
		[HttpGet]
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
		//public IActionResult Coming_Soon()
		//{
		//	return View();
		//}
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
		public IActionResult BooksGridView(ViewUserBooks vub,AddCategory Aac, string categoryFilter)
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
			//if (!string.IsNullOrEmpty(categoryFilter))
			//{
			//	// Apply the category filter to your books dataset
			//	ds.Tables[0].DefaultView.RowFilter = $"category = '{categoryFilter}'";
			//	ds.Tables[0] = ds.Tables[0].DefaultView.ToTable();
			//}

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
					string orderstatus = "pending".ToString();
					string paymentstatus = "pending".ToString();
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
						atc.AddtoCartData(userId, bookName, bookPrice, bookQuantity, bookImg,orderstatus,paymentstatus);
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
			int userId = int.Parse((string)TempData.Peek("UserLogin_id"));
			DataSet ds = atc.selectUserWithJoin(userId);
			ViewBag.OrderData = ds.Tables[0];
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
            return View();
        }
        [HttpPost]
		public IActionResult OrderPage(CardDetails cd , AddtoCart atc)
		{
			string paymentstatus = "completed";

			try
			{
				string userId = TempData.Peek("UserLogin_id").ToString();

				atc.PaymentStatusUpdate(userId, paymentstatus);
				cd.AddCardDetails(cd.CardName, cd.CardNum, cd.CardVerifyNum);
				return RedirectToAction("ViewOrders");
			}
			catch (Exception ex)
			{
				// Log the exception for debugging
				Console.WriteLine("Error updating payment status: " + ex.Message);
				return RedirectToAction("ErrorPage"); // Redirect to an error page
			}
		}
		[HttpGet]
		public IActionResult OrderHistory(AddtoCart atc)
		{
			if (TempData.Peek("UserLogin_id") != null)
			{
				int userId = int.Parse((string)TempData.Peek("UserLogin_id"));
				DataSet ds = atc.selectUserWithJoin(userId);
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
			int userId = int.Parse((string)TempData.Peek("UserLogin_id"));
			DataSet ds = atc.selectUserWithJoin(userId);
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
		public IActionResult BlogDetail(AdminData ad)
		{
			ad.AddNewCommwnt(ad.author, ad.email, ad.comment);
			return RedirectToAction("BlogDetail");
		}
		[HttpGet]
		public IActionResult BlogDetail()
		{
			return View();
		}
		public IActionResult EmptyCartPage()
		{
			return View();	
		}
		public IActionResult ErrorPage()
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