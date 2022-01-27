using System;
using System.Data;
using System.Data.SqlClient;

namespace Phews
{
    public partial class WebForm10 : System.Web.UI.Page
    {
        private string connectionString = "Data Source= LAPTOP-4QHRG8E2; Integrated Security=true;Initial Catalog= PhewsDB;";

        protected void Page_Load(object sender, EventArgs e)
        {
            displayStats();
        }

        private void displayStats()
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                //display number of articles
                SqlCommand sqlcmdA = new SqlCommand("spAdminStatsArticles", sqlcon);
                sqlcmdA.CommandType = CommandType.StoredProcedure;
                sqlcmdA.Parameters.AddWithValue("@User_ID", Session["ID"]);
                string a = Convert.ToString(sqlcmdA.ExecuteScalar());
                a = a + " Articles";
                pArticlesPublished.InnerText = a;
                //display number of Photos
                SqlCommand sqlcmdP = new SqlCommand("spAdminStatsPhotos", sqlcon);
                sqlcmdP.CommandType = CommandType.StoredProcedure;
                sqlcmdP.Parameters.AddWithValue("@User_ID", Session["ID"]);
                string p = Convert.ToString(sqlcmdP.ExecuteScalar());
                p = p + " Photos";
                pPhotosPublished.InnerText = p;
                //display number of Users
                SqlCommand sqlcmdU = new SqlCommand("spAdminStatsUsers", sqlcon);
                sqlcmdU.CommandType = CommandType.StoredProcedure;

                string u = Convert.ToString(sqlcmdU.ExecuteScalar());
                u = u + " Users";
                pNumberOfUsers.InnerText = u;
                sqlcon.Close();
            }
        }
    }
}