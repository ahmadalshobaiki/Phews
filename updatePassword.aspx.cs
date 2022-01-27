using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Phews
{
    public partial class WebForm19 : System.Web.UI.Page
    {
        void Page_PreInit(Object sender, EventArgs e)
        {
            if (Session["role"] == null)
            {
                Response.Redirect("signup.aspx");
            }
            else if (Methods.isAdmin() || Methods.isSuperAdmin())
            {
                this.MasterPageFile = "~/admin.master";
            }
           
            else
            {
                this.MasterPageFile = "~/phews.master";
            }
        }
        static string conString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        SqlConnection conn = new SqlConnection(conString);

        SqlCommand cmd;

        protected void Page_Load(object sender, EventArgs e)
        {
            // first check if session exists, if it doesn't then redirect the user back to the login screen
            if (System.Web.HttpContext.Current.Session["username"] == null)
            {
                Response.Redirect("signup.aspx");
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {

            // check if old password entered by the user is the same one as in the database
            if (oldPassMatch())
            {
                lbl_oldMsg.Visible = false;
                // validate then upate password
                if (!HasEmptyFields())
                {
                    lbl_generalMsg.Visible = false;
                    if (IsPasswordValid())
                    {
                        lbl_newMsg.Visible = false;
                        if (IsPasswordFieldsMatch())
                        {
                            lbl_confirmMsg.Visible = false;
                            updatePassword();
                        }
                        else
                        {
                            // the new and old password fields does not match
                            lbl_confirmMsg.Attributes.Add("style", "visibility: visible");
                            lbl_confirmMsg.Attributes.Add("style", "color: red");
                            lbl_confirmMsg.Text = "Password does not match";
                        }
                    }
                }
                else
                {
                    // there is one or more empty fields
                    lbl_generalMsg.Attributes.Add("style", "visibility: visible");
                    lbl_generalMsg.Attributes.Add("style", "color: red");
                    lbl_generalMsg.Text = "One or more input fields are empty!";
                }
            }
            else
            {
                // the old password does not match the database password

                lbl_oldMsg.Attributes.Add("style", "visibility: visible");
                lbl_oldMsg.Attributes.Add("style", "color: red");
                lbl_oldMsg.Text = "Your old password is incorrect!";
            }
        }

        private bool oldPassMatch()
        {
            conn.Open();
            cmd = new SqlCommand("checkUserExists", conn);
            cmd.Parameters.AddWithValue("@Username", Session["username"].ToString());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(table);

            if (txt_old.Text == table.Rows[0][2].ToString())
            {
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                return false;
            }
        }

        private bool HasEmptyFields()
        {
            if (txt_old.Text == "" || txt_new.Text == "" || txt_confirm.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsPasswordValid()
        {
            // write all regex patterns here
            Regex hasMiniMaxChars = new Regex(@".{8,15}");
            Regex hasLetter = new Regex(@"[a-zA-Z]+");
            Regex hasNumber = new Regex(@"[0-9]+");
            Regex hasEmoji = new Regex(@".{U+1F600, U+1F64F}"); // does not work


            if (!hasMiniMaxChars.IsMatch(txt_new.Text))
            {
                lbl_newMsg.Attributes.Add("style", "visibility: visible");
                lbl_newMsg.Attributes.Add("style", "color: red");
                lbl_newMsg.Text = "Your password must be 8-20 characters long";
                return false;
            }
            else if (!hasLetter.IsMatch(txt_new.Text))
            {
                lbl_newMsg.Attributes.Add("style", "visibility: visible");
                lbl_newMsg.Attributes.Add("style", "color: red");
                lbl_newMsg.Text = "Your password must contain atleast 1 letter";
                return false;
            }
            else if (!hasNumber.IsMatch(txt_new.Text))
            {
                lbl_newMsg.Attributes.Add("style", "visibility: visible");
                lbl_newMsg.Attributes.Add("style", "color: red");
                lbl_newMsg.Text = "Your password must contain atleast 1 number";
                return false;
            }
            else if (hasEmoji.IsMatch(txt_new.Text))
            {
                lbl_newMsg.Attributes.Add("style", "visibility: visible");
                lbl_newMsg.Attributes.Add("style", "color: red");
                lbl_newMsg.Text = "Your password cannot contain  emojis";
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsPasswordFieldsMatch()
        {
            if (txt_new.Text != txt_confirm.Text)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void updatePassword()
        {
            conn.Open();
            string updatePassword = "UPDATE Users SET Password = @Password WHERE ID = @ID AND Username = @Username";
            cmd = new SqlCommand(updatePassword, conn);
            cmd.Parameters.AddWithValue("@Password", txt_new.Text);
            cmd.Parameters.AddWithValue("@ID", Session["ID"]);
            cmd.Parameters.AddWithValue("@Username", Session["username"]);
            cmd.ExecuteNonQuery();
            conn.Close();

            lbl_generalMsg.Attributes.Add("style", "visibility: visible");
            lbl_generalMsg.Attributes.Add("style", "color: green");
            lbl_generalMsg.Text = "Your password has been updated!";

            Response.Redirect("myProfile.aspx");
        }


    }
}