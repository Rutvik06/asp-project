﻿using System.Data;
using System.Data.SqlClient;

namespace template.Models
{
	public class Add_Profile
	{
		public int id { get; set; }
		public string name { get; set; }
		public string profession { get; set; }
		public string language { get; set; }
		public int age { get; set; }
		public string contact { get; set; }
		public string email { get; set; }
		public string country { get; set; }
		public string pincode { get; set; }
		public string address { get; set; }
		public string city { get; set; }
		public int userId { get; set; }
		public string profileimg { get; set; }

		SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;database=project;User Id=sa;pwd=12345");
		public int AddNewProfile(string name, string profession, string language, int age, string contact, string email, string country, string pincode, string address, string city,string userId,string profileimg)
		{
			SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Add_Profile] (name, profession, language, age, contact, email,country,pincode,address,city,userId,profileimg) " +
											"VALUES ('" + name + "', '" + profession + "', '" + language + "', '" + age + "', '" + contact + "', '" + email + "','" + country + "','" + pincode + "','" + address + "','" + city + "','"+userId+"','"+ profileimg + "')", con);
			con.Open();
			return cmd.ExecuteNonQuery();
		}
		public DataSet selectUpdateProfile(int userId)
		{
			SqlCommand cmd = new SqlCommand("select * from[dbo].[Add_Profile] where userId='" + userId + "'", con);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);
			return ds;
		}
	}
}
