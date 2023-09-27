using System.Data;
using System.Data.SqlClient;

namespace template.Models
{
    public class AddtoCart
    {
        public int id { get; set; }
		public string userId { get; set; }
        public string BookName { get; set; }
        public int BookQuantity { get; set; }
        public string BookPrice { get; set; }
        public string BookImg { get; set;}
		public string OrderStatus { get; set; }
		public string PaymentStatus { get;set; }


		SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345");
		//public int AddtoCartData(string UserId, string BookName, string BookPrice,string BookQuantity, string BookImg,DateTime AddedOn)
		//{
		//    // Use parameterized query to prevent SQL injection

		//    SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Add_To_Cart] (BookName, UserId, BookPrice,BookImg,BookQuantity,AddedOn) " +
		//                                    "VALUES ('" + BookName + "', '" + UserId + "', '" + BookPrice + "', '" + BookImg + "', '" + BookQuantity + "','"+ AddedOn + "')", con);
		//    con.Open();



		//    return cmd.ExecuteNonQuery();

		//}
		public int AddtoCartData(string userId,string BookName, string BookPrice, string BookQuantity, string BookImg,string OrderStatus, string PaymentStatus)
		{
			SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Add_To_Cart] (userId,BookName, BookPrice, BookImg, BookQuantity,OrderStatus,PaymentStatus) " +
											"VALUES ('"+ userId + "','"+BookName+"',  '"+BookPrice+"', '"+BookImg+"', '"+BookQuantity+"','"+OrderStatus+"','"+ PaymentStatus + "')", con);
			con.Open();
			return cmd.ExecuteNonQuery();
		}

		public DataSet selectAddedBook(int id)
		{
			SqlCommand cmd = new SqlCommand("select * from[dbo].[Add_To_Cart] where id='"+id+"'", con);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);

			return ds;
		}
		public DataSet selectWithUserId()
		{
			SqlCommand cmd = new SqlCommand("select * from[dbo].[Add_To_Cart]", con);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);
			return ds;
		}
		public int deleteCart(int id)
		{
			SqlCommand cmd = new SqlCommand("delete from [dbo].[Add_To_Cart] where id='" + id + "'", con);
			con.Open();

			return cmd.ExecuteNonQuery();
		}

		public DataSet GetCartItems(string userId,string BookName)
		{
			SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Add_To_Cart] where userId = '"+userId+"' and BookName = '"+BookName+"' ", con);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);
			return ds;
		}

		public int UpdateCartItemQuantity(string userId,string BookName,int BookQuantity)
		{
			SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Add_To_Cart] SET BookQuantity = '"+BookQuantity + "' WHERE userId = '"+userId + "' AND BookName = '"+BookName + "'", con);

			con.Open();
			return cmd.ExecuteNonQuery();
		}

		public DataSet selectUser(string userId)
		{
			SqlCommand cmd = new SqlCommand("select * from [dbo].[Add_To_Cart] where userId='" + userId + "'",con);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);
			return ds;
		}

		public DataSet selectUserWithJoin(int userId)
		{
			SqlCommand cmd = new SqlCommand("SELECT c.* FROM [dbo].[Add_To_Cart] c INNER JOIN [dbo].[User_Login] l ON c.userId = l.id WHERE l.id = '"+userId+"'", con);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);
			return ds;
		}
		public DataSet PaymentStatusUpdate(string userId, string PaymentStatus)
		{
			SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Add_To_Cart] SET PaymentStatus = '" + PaymentStatus + "' WHERE userId = '" + userId + "' ", con);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);
			return ds;
		}
		



	}
}
