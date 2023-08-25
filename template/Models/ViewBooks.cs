using System.Data.SqlClient;
using System.Data;

namespace template.Models
{
    public class ViewBooks
    {
        public int id { get; set; }
        public string BookName { get; set; }
        public string BookCategory { get; set; }
        public string BookAuthor { get; set; }
        public string BookImage { get; set; }
        public string BookPrice { get; set; }
        public string BookDescription { get; set; }
        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345");

        public DataSet selectNewBook()
        {
            SqlCommand cmd = new SqlCommand("select * from[dbo].[Add_Book]", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }
        public int deleteBook(int id)
        {
            SqlCommand cmd = new SqlCommand("delete from [dbo].[Add_Book] where id='" + id + "'", con);
            con.Open();

            return cmd.ExecuteNonQuery();
        }
        public int updateBook(int id,string BookName, string BookCategory, string BookPrice, string BookDescription, string BookAuthor, string BookImage)
        {
            SqlCommand cmd = new SqlCommand("update [dbo].[Add_Book] set BookName='" + BookName + "',BookCategory='" + BookCategory + "' ,BookPrice='" + BookPrice + "' ,BookDescription='" + BookDescription + "' ,BookAuthor='" + BookAuthor + "' ,BookImage='" + BookImage + "' where id='" + id + "'", con);
            con.Open();

            return cmd.ExecuteNonQuery();
        }
       
    }
}
