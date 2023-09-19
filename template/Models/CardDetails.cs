using System.Data;
using System.Data.SqlClient;

namespace template.Models
{
	public class CardDetails
	{
		public int id { get; set; }
		public string CardName { get; set; }
		public string CardNum { get; set; }
		public string CardVerifyNum { get; set; }
		SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345");
		public int AddCardDetails(string CardName, string CardNum, string CardVerifyNum)
		{
			SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Card_Details] (CardName, CardNum, CardVerifyNum) " +
											"VALUES ('" + CardName + "',  '" + CardNum + "', '" + CardVerifyNum + "')", con);
			con.Open();
			return cmd.ExecuteNonQuery();
		}

		
	}
}
