using System.Data.SqlClient;
using System.Data;
namespace template.Models
{
	public class Order
	{
		public int id { get; set; }
		//public string userId { get; set; }
		//public string[] BookName { get; set; } // Define as an array
		//public int[] BookQuantity { get; set; } // Define as an array
		//public string[] BookPrice { get; set; } // Define as an array
		//public string[] BookImg { get; set; } // Define as an array
		public string OrderStatus { get; set; }
		SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345");
		//public int AddToOrder(string userId, string BookName, string BookPrice, int BookQuantity, string BookImg,string OrderStatus)
		//{
		//	SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Add_Order] (userId,BookName, BookPrice, BookImg, BookQuantity,OrderStatus) " +
		//									"VALUES ('" + userId + "','" + BookName + "',  '" + BookPrice + "', '" + BookImg + "', '" + BookQuantity + "','"+ OrderStatus + "')", con);
		//	con.Open();
		//	return cmd.ExecuteNonQuery();
		//}
		//public void CloseConnection()
		//{
		//	if (con.State == ConnectionState.Open)
		//	{
		//		con.Close();
		//	}
		//}
		public DataSet ViewAdminOrder()
		{
			
				SqlCommand cmd = new SqlCommand("select * from[dbo].[Add_To_Cart]", con);
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				DataSet ds = new DataSet();
				da.Fill(ds);

				return ds;
			
		}
        public DataSet UpdateOrderId(int id)
        {

            SqlCommand cmd = new SqlCommand("select * from[dbo].[Add_To_Cart] where id='" + id + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;

        }
        public int updateStatus(int id, string OrderStatus)
        {
            SqlCommand cmd = new SqlCommand("update [dbo].[Add_To_Cart] set OrderStatus='" + OrderStatus + "'where id='" + id + "'", con);
            con.Open();

            return cmd.ExecuteNonQuery();
        }
    }
}
