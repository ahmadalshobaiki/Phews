using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Phews
{
    public partial class WebForm20 : System.Web.UI.Page
    {
        

        static string conString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        SqlConnection conn = new SqlConnection(conString);

        SqlCommand cmd;

        protected void Page_Load(object sender, EventArgs e)
        {
            int articleID = Convert.ToInt32(Request.QueryString["ID"]);
            
            conn.Open();
            cmd = new SqlCommand("spDisplayNewsArticle", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", articleID);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                header.InnerText = reader.GetString(1);
                subtitle.InnerText = reader.GetString(2);
                postedBy.InnerText = reader.GetString(4);
                postedOn.InnerText = reader.GetDateTime(5).ToString("dd-MM-yyyy");
                image.Attributes.Add("src", ("~/img/" + reader.GetString(3)));
                articleText.InnerText = reader.GetString(6);

            }
            




        }


    }
}