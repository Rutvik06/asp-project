using System.Data.SqlClient;

namespace template.Models
{
	public class UserAccount
	{
		public int id { get; set; }
		public string UserFName { get; set; }
		public string UserLName { get; set; }
		public string UserCountry { get; set; }
		public string UserEmail { get; set; } 
		public string UserPhone { get; set; }	
		public string UserAddress { get; set; }
		public string UserCity { get; set; }
		public string UserZip { get; set; }

		SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345");
		public int AddAccount(string UserFName, string UserLName, string UserCountry, string UserEmail, string UserPhone, string UserAddress,string UserCity,string UserZip)
		{
			// Use parameterized query to prevent SQL injection

			SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Add_Account] (UserFName, UserLName, UserCountry, UserEmail, UserPhone, UserAddress,UserCity,UserZip) " +
											"VALUES ('" + UserFName + "', '" + UserLName + "', '" + UserCountry + "', '" + UserEmail + "', '" + UserPhone + "', '" + UserAddress + "','"+ UserCity + "','"+UserZip+"')", con);
			con.Open();



			return cmd.ExecuteNonQuery();

		}
	}
}
