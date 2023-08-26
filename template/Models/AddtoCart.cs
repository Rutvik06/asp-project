using System.Data.SqlClient;

namespace template.Models
{
    public class AddtoCart
    {
        public int id { get; set; }
        public string UserId { get; set; }
        public string BookName { get; set; }
        public string BookQuantity { get; set; }
        public string BookPrice { get; set; }
        public string BookImg { get; set;}
        public DateTime Addedon { get; set; }

        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345");
        public int AddtoCartData(string UserId, string BookName, string BookPrice,string BookQuantity, string BookImg,DateTime AddedOn)
        {
            // Use parameterized query to prevent SQL injection

            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Add_To_Cart] (BookName, UserId, BookPrice,BookImg,BookQuantity,AddedOn) " +
                                            "VALUES ('" + BookName + "', '" + UserId + "', '" + BookPrice + "', '" + BookImg + "', '" + BookQuantity + "','"+ AddedOn + "')", con);
            con.Open();



            return cmd.ExecuteNonQuery();

        }
    }
}
