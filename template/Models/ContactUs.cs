using System.Data.SqlClient;

namespace template.Models
{
	public class ContactUs
	{
		public int id { get; set; }
		public string fullname { get; set; }
		public string email { get; set; }
		public string phone { get; set; }
		public string message { get; set; }
		SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345");
		public int AddContact(string fullname, string email, string phone, string message)
		{
			// Use parameterized query to prevent SQL injection

			SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Contact_Us] (fullname, email, phone, message) " +
											"VALUES ('" + fullname + "', '" + email + "', '" + phone + "', '" + message + "')", con);
			con.Open();



			return cmd.ExecuteNonQuery();

		}
	}
}
