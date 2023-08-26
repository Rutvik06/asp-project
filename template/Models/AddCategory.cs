using System.Data;
using System.Data.SqlClient;

namespace template.Models
{
    public class AddCategory
    {
        public int id { get; set; }
        public string category { get; set; }
        public string description { get; set; }

        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345");
        public int AddNewCategory(string category,string description)
        {
            // Use parameterized query to prevent SQL injection

            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Add_Category] (category,description) " + "VALUES ('" + category + "','"+description+"')", con);
            con.Open();
            return cmd.ExecuteNonQuery();

        }
        public DataSet selectNewCategory()
        {
            SqlCommand cmd = new SqlCommand("select * from[dbo].[Add_Category]", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }
        public int deleteNewCategory(int id)
        {
            SqlCommand cmd = new SqlCommand("delete from [dbo].[Add_Category] where id='" + id + "'", con);
            con.Open();

            return cmd.ExecuteNonQuery();
        }
        public int updateNewCategory(int id, string category, string description)
        {
            SqlCommand cmd = new SqlCommand("update [dbo].[Add_Category] set category='" + category + "',description='" + description + "' where id='" + id + "'", con);
            con.Open();

            return cmd.ExecuteNonQuery();
        }
		public DataSet selectSinCategory(int id )
		{
			SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Add_Category] WHERE id=@id", con);
			cmd.Parameters.AddWithValue("@id", id);

			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);

			return ds;
		}
	}
}
