using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections;

namespace Phews
{


    public partial class Site1 : System.Web.UI.MasterPage
    {

        static string conString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        SqlConnection conn = new SqlConnection(conString);

        SqlCommand cmd;

        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void LoginClicked(object sender, EventArgs e)
        {
            if (!HasEmptyFields())
            {
                if (DoesUserExists())
                {
                    LoginUser();
                }
                else
                {
                    error_msg.Attributes.Add("style", "visibility: visible");
                    error_msg.Attributes.Add("style", "color: red");
                    error_msg.Text = "Your credentials does not exist!";

                }
            }
            else
            {
                error_msg.Attributes.Add("style", "visibility: visible");
                error_msg.Attributes.Add("style", "color: red");
                error_msg.Text = "There are empty fields!";
            }
        }

        
        // if there are no problems, login the user and start session
        private void LoginUser()
        {
            conn.Open();
            cmd = new SqlCommand("loginUser", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Session["ID"] = reader.GetInt32(0);
                Session["username"] = reader.GetString(1);
                try { Session["fname"] = reader.GetString(3); } catch (System.Data.SqlTypes.SqlNullValueException) { Session["fname"] = ""; }
                try { Session["lname"] = reader.GetString(3); } catch (System.Data.SqlTypes.SqlNullValueException) { Session["lname"] = ""; }
                try { Session["email"] = reader.GetString(3); } catch (System.Data.SqlTypes.SqlNullValueException) { Session["email"] = ""; }
                try { Session["phone"] = reader.GetString(3); } catch (System.Data.SqlTypes.SqlNullValueException) { Session["phone"] = ""; }
                try { Session["gender"] = reader.GetString(3); } catch (System.Data.SqlTypes.SqlNullValueException) { Session["gender"] = ""; }
                try { Session["dob"] = reader.GetString(3); } catch (System.Data.SqlTypes.SqlNullValueException) { Session["dob"] = ""; }
                int ID = reader.GetInt32(0);
                conn.Close();
                conn.Open();
                SqlCommand cmd1 = new SqlCommand("spDisplayRoles", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@ID", ID );
                SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ArrayList arrayList = new ArrayList();
                for ( int i = 0; i <dt.Rows.Count; i++)
                {
                    arrayList.Add(dt.Rows[i][0]);
                }
                Session["role"] = arrayList;

                conn.Close();
                if (Methods.isAdmin() || Methods.isSuperAdmin())
                {
                    Response.Redirect("Home_Admin.aspx", true);
                }
                else
                {
                    Response.Redirect("Home_General.aspx", true);
                }
               
            }
            else
            {
                error_msg.Attributes.Add("style", "visibility: visible");
                error_msg.Attributes.Add("style", "color: red");
                error_msg.Text = "Your credentials are wrong!";
            }
        }
   
        private bool HasEmptyFields()
        {
            if (txtUsername.Text == "" || txtPassword.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        private bool DoesUserExists()
        {
            
            conn.Open();
            cmd = new SqlCommand("checkUserExists", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                reader.Close();
                conn.Close();
                return true;
            }
            else
            {
                reader.Close();
                conn.Close();
                return false;
            }

            

            //if (cmd.ExecuteNonQuery() == 0)
            //{
            //    // user exists
            //    conn.Close();
            //    return true;
            //}
            //else
            //{
            //    // user does not exists
            //    conn.Close();
            //    return false;
            //}
            
        }
        
    }
}