using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Text.RegularExpressions;


namespace Phews
{
    public partial class WebForm15 : System.Web.UI.Page
    {

        static string conString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        SqlConnection conn = new SqlConnection(conString);

        SqlCommand cmd; 

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Signup(object sender, EventArgs e)
        {
            
            /* 
            TODO:
            1- Check if password is valid -> just check for emojis using regex

            DONE:
            - Check if there are empty input fields
            - Check if password fields match
            - Check if email is valid
            - Check if username exists
            - Check if email is taken

            */

            // check if there are empty input fields
            if (!HasEmptyFields())
            {
                // check if password is valid
                if (IsPasswordValid())
                {
                    error_pass.Visible = false;
                    // check if password fields match
                    if (IsPasswordFieldsMatch())
                    {
                        error_confirm.Visible = false;
                        // check if email is available
                        if (IsEmailAvailable())
                        {
                            error_email.Visible = false;
                            // check if email is valid
                            if (IsValidEmail())
                            {
                                error_email.Visible = false;
                                if(IsCheckboxSelected())
                                {
                                    err_role.Visible = false;
                                    // create the user
                                    CreateUser();
                                    error_empty.Attributes.Add("style", "visibility: visible");
                                    error_empty.Attributes.Add("style", "color: green");
                                    error_empty.Text = "Your account has been created!";
                                    clear();
                                }
                                else
                                {
                                    // checkboxes were not selected
                                    err_role.Attributes.Add("style", "visibility: visible");
                                    err_role.Attributes.Add("style", "color: red");
                                    err_role.Text = "Please select a prefrence";
                                }
                           
                            }
                            else
                            {
                                // email is invalid
                                error_email.Visible = true;
                              
                                error_email.Text = "Email is invalid";
                            }
                        }
                        else
                        {
                            // email is unavailable
                            error_email.Attributes.Add("style", "visibility: visible");
                            error_email.Attributes.Add("style", "color: red");
                            error_email.Text = "Email is already taken";
                        }
                    }
                    else
                    {
                        // password fields does not match
                        error_confirm.Attributes.Add("style", "visibility: visible");
                        error_confirm.Attributes.Add("style", "color: red");
                        error_confirm.Text = "Password does not match";
                    }
                }  
            }
            // some input fields are empty
            else
            {
                error_empty.Attributes.Add("style", "visibility: visible");
                error_empty.Attributes.Add("style", "color: red");
                error_empty.Text = "One or more input fields are empty!";
            }
        }

        private bool IsPasswordValid()
        {
            // write all regex patterns here
            Regex hasMiniMaxChars = new Regex(@".{8,15}");
            Regex hasLetter = new Regex(@"[a-zA-Z]+");
            Regex hasNumber = new Regex(@"[0-9]+");
            Regex hasEmoji = new Regex(@".{U+1F600, U+1F64F}"); // does not work


            if (!hasMiniMaxChars.IsMatch(password_signup.Text))
            {
                error_pass.Attributes.Add("style", "visibility: visible");
                error_pass.Attributes.Add("style", "color: red");
                error_pass.Text = "Your password must be 8-20 characters long";
                return false;
            }
            else if (!hasLetter.IsMatch(password_signup.Text))
            {
                error_pass.Attributes.Add("style", "visibility: visible");
                error_pass.Attributes.Add("style", "color: red");
                error_pass.Text = "Your password must contain atleast 1 letter";
                return false;
            }
            else if (!hasNumber.IsMatch(password_signup.Text))
            {
                error_pass.Attributes.Add("style", "visibility: visible");
                error_pass.Attributes.Add("style", "color: red");
                error_pass.Text = "Your password must contain atleast 1 number";
                return false;
            }
            else if (hasEmoji.IsMatch(password_signup.Text))
            {
                error_pass.Attributes.Add("style", "visibility: visible");
                error_pass.Attributes.Add("style", "color: red");
                error_pass.Text = "Your password cannot contain  emojis";
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsValidEmail()
        {
            bool result = true;
            try
            {
                string testString = email_signup.Text;
                MailAddress mail = new MailAddress(testString);
                bool isValidEmail = mail.Host.Contains(".");
                if (!isValidEmail)
                {
                    result = false;
                    
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        private bool HasEmptyFields()
        {
            if (username_signup.Text == "" || email_signup.Text == "" || password_signup.Text == "" || confirm_pass.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsPasswordFieldsMatch()
        {
            if (password_signup.Text != confirm_pass.Text)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsEmailAvailable()
        {
            conn.Open();
            cmd = new SqlCommand("SELECT count(Email) as Email from Users where Email = @Email", conn);
            cmd.Parameters.AddWithValue("@Email", email_signup.Text);
            string emailCheck = cmd.ExecuteScalar().ToString();

            if (!(emailCheck == "0"))
            {
                // email is taken
                conn.Close();
                return false;
                
            }
            else
            {
                conn.Close();
                return true;
            }
            
        }
        private bool IsCheckboxSelected()
        {
            for (int i = 0; i < cblPreference.Items.Count; i++)
            {
                if (cblPreference.Items[i].Selected)
                {
                    return true;
                }
            }
           
                return false;
           

        }

        private void CreateUser()
        {
            int Photos = 0; int News=0;
            if(cblPreference.Items[0].Selected)
            {
                Photos = 1;
            }
            if (cblPreference.Items[1].Selected)
            {
                News = 1;
            }
            try
            {
                // STORED PROCEDURE
                conn.Open();

                cmd = new SqlCommand("Create_User", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username_signup.Text);
                cmd.Parameters.AddWithValue("@Email", email_signup.Text);
                cmd.Parameters.AddWithValue("@Password", password_signup.Text);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("spAssignNewUserRole", conn);
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Username", username_signup.Text);
                cmd1.Parameters.AddWithValue("@Photos", Photos);
                cmd1.Parameters.AddWithValue("@News", News);
                cmd1.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {

                // Check if it is an SQL Exception, then handle it

                if (ex.GetBaseException().GetType() == typeof(SqlException))
                {
                    Int32 ErrorCode = ((SqlException)ex.GetBaseException()).Number;
                    switch (ErrorCode)
                    {
                        case 2627:
                            // unique key violation
                            // username is not unique
                            error_username.Attributes.Add("style", "visibility: visible");
                            error_username.Attributes.Add("style", "color: red");
                            error_username.Text = "Username is already taken";
                            break;
                        case 547:
                            // constraint check violation
                            break;
                        case 2601:
                            // Duplicated row key error
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    // handle normal exception if it is not an SQL exception
                    error_empty.Attributes.Add("style", "visibility: visible");
                    error_empty.Attributes.Add("style", "color: red");
                    error_empty.Text = "Something went wrong";
                }
            }
        }
        private void clear()
        {
            username_signup.Text = null;
            email_signup.Text = "";
            for (int i = 0; i < cblPreference.Items.Count; i++)
            {
                cblPreference.Items[i].Selected = false;
  
            }
        }
    }
}