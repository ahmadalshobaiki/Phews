using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhoneNumbers;

namespace Phews
{
    /*
     TO DO:
        1- change all HTML form controls to ASP form controls -> done
        2- Fill up the form with user data from current session -> done
        3- Validate form on click save -> phone numbers
     */

    public partial class WebForm14 : System.Web.UI.Page
    {
        void Page_PreInit(Object sender, EventArgs e)
        {
            if(Session["role"] == null)
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
            if (System.Web.HttpContext.Current.Session["username"] == null)
            {
                Response.Redirect("signup.aspx");
            }

            if (!IsPostBack)
            {
                FillForm();

            }  
        }

        

        protected void Btn_Clear_Click(object sender, EventArgs e)
        {
            FillForm();
        }

        protected void Btn_Save_Click(object sender, EventArgs e)
        {
            UpdateFname();
            UpdateLname();
            UpdateEmail();
            UpdatePhone();
            UpdateGender();
            UpdateDOB();
            UpdateDatabaseRole();
            UpdateSession();
            FillForm();

        }

        private void UpdateDatabaseRole()
        {
            if(IsCheckboxSelected())
            {
                conn.Open();
                int ID = Convert.ToInt32(Session["ID"]);
                for (int i = 0; i < cblPreference.Items.Count; i++)
                {
                    if (cblPreference.Items[i].Selected)
                    {
                        cmd = new SqlCommand("spAddRole", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@RoleID", (i + 2));
                        cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                        cmd.Parameters.AddWithValue("@created_By", Session["username"].ToString());
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd = new SqlCommand("spDeleteRole", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@RoleID", (i + 2));
                        cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
                lbl_cb_msg.Text = "Your preferences have been saved";
                lbl_cb_msg.Visible = true;
                lbl_cb_msg.Attributes.Add("style", "color:green");

               
            }
            else
            {
                lbl_cb_msg.Text = "At least one preference must be selected";
                lbl_cb_msg.Visible = true;
                lbl_cb_msg.Attributes.Add("style", "color:red");
            }
           

        }

        private void UpdateSession()
        {
            SqlCommand cmd1 = new SqlCommand("spDisplayRoles", conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@ID", Session["ID"].ToString());
            SqlDataAdapter sda = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ArrayList arrayList = new ArrayList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                arrayList.Add(dt.Rows[i][0]);
            }
            Session["role"] = arrayList;
        }

        private void FillForm()
        {
            

            conn.Open();
            cmd = new SqlCommand("checkUserExists", conn);
            cmd.Parameters.AddWithValue("@Username", Session["username"].ToString());
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(table);

            // fill the form with user data
            txt_username.Text = Session["username"].ToString();           
            txt_fname.Text = table.Rows[0][3].ToString();
            txt_lname.Text = table.Rows[0][4].ToString();
            txt_email.Text = table.Rows[0][5].ToString();
            txt_phone.Text = table.Rows[0][6].ToString();
            drp_gender.Value = table.Rows[0][7].ToString();
            //we have to fix this 
            if(table.Rows[0][8].ToString() != "")
            {
                txt_dob.Text = DateTime.Parse(table.Rows[0][8].ToString()).ToString("yyyy-MM-dd");
            }


            if (Methods.isUserP())
            {
                cblPreference.Items[0].Selected = true;
            }
            if (Methods.isUserN())
            {
                cblPreference.Items[1].Selected = true;
            }








        }

        private void UpdateDOB()
        {
            if(txt_dob.Text != "")
            {
                conn.Open();
                string updateDOB = "UPDATE Users SET DOB = @DOB, Modify_Date = @Date WHERE ID = @ID AND Username = @Username";
                cmd = new SqlCommand(updateDOB, conn);
                cmd.Parameters.AddWithValue("@DOB", txt_dob.Text);
                cmd.Parameters.AddWithValue("@ID", Session["ID"]);
                cmd.Parameters.AddWithValue("@Username", Session["username"]);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.ExecuteNonQuery();
                conn.Close();
                Session["dob"] = txt_dob.Text;

                lbl_dobMsg.Attributes.Add("style", "visibility: visible");
                lbl_dobMsg.Attributes.Add("style", "color: green");
                lbl_dobMsg.Text = "Your DOB has been saved!";

            }
     
        }

        private void UpdateGender()
        {
            if(drp_gender.Value != "none")
            {
                conn.Open();
                string updateGender = "UPDATE Users SET Gender = @Gender, Modify_Date = @Date WHERE ID = @ID AND Username = @Username";
                cmd = new SqlCommand(updateGender, conn);
                cmd.Parameters.AddWithValue("@Gender", drp_gender.Value);
                cmd.Parameters.AddWithValue("@ID", Session["ID"]);
                cmd.Parameters.AddWithValue("@Username", Session["username"]);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.ExecuteNonQuery();
                conn.Close();
                Session["gender"] = drp_gender.Value;

                lbl_genderMsg.Attributes.Add("style", "visibility: visible");
                lbl_genderMsg.Attributes.Add("style", "color: green");
                lbl_genderMsg.Text = "Saved!";
            }
         
        }

        private void UpdatePhone()
        {
            // validate then update database record
            if(txt_phone.Text != "")
            {
                PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
                var phoneNumber = phoneNumberUtil.Parse(txt_phone.Text, "SA");


                if (phoneNumberUtil.IsValidNumber(phoneNumber))
                {
                    conn.Open();
                    string updatePhone = "UPDATE Users SET Phone = @Phone, Modify_Date = @Date WHERE ID = @ID AND Username = @Username";
                    cmd = new SqlCommand(updatePhone, conn);
                    cmd.Parameters.AddWithValue("@Phone", txt_phone.Text);
                    cmd.Parameters.AddWithValue("@ID", Session["ID"]);
                    cmd.Parameters.AddWithValue("@Username", Session["username"]);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Session["phone"] = txt_phone.Text;

                    lbl_phoneMsg.Attributes.Add("style", "visibility: visible");
                    lbl_phoneMsg.Attributes.Add("style", "color: green");
                    lbl_phoneMsg.Text = "Your phone no. has been saved!";
                }
                else
                {
                    lbl_phoneMsg.Attributes.Add("style", "visibility: visible");
                    lbl_phoneMsg.Attributes.Add("style", "color: red");
                    lbl_phoneMsg.Text = "Your phone no. is not valid!";
                }
            }
         
        }

        private void UpdateEmail()
        {
            // validate then update database record
            if (IsValidEmail())
            {
                conn.Open();
                string updateEmail = "UPDATE Users SET Email = @Email, Modify_Date = @Date WHERE ID = @ID AND Username = @Username";
                cmd = new SqlCommand(updateEmail, conn);
                cmd.Parameters.AddWithValue("@Email", txt_email.Text);
                cmd.Parameters.AddWithValue("@ID", Session["ID"]);
                cmd.Parameters.AddWithValue("@Username", Session["username"]);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.ExecuteNonQuery();
                conn.Close();
                Session["email"] = txt_email.Text;

                lbl_emailMsg.Attributes.Add("style", "visibility: visible");
                lbl_emailMsg.Attributes.Add("style", "color: green");
                lbl_emailMsg.Text = "Your email has been saved!";
            }
            else
            {
                lbl_emailMsg.Attributes.Add("style", "visibility: visible");
                lbl_emailMsg.Attributes.Add("style", "color: red");
                lbl_emailMsg.Text = "Your email is not valid!!";
            }
        }

        private void UpdateLname()
        {
            if(txt_lname.Text != "")
            {
                // validate then update database record
                Regex isValidLname = new Regex("^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+");
                Regex hasNumber = new Regex(@"[0-9]+");

                if (isValidLname.IsMatch(txt_lname.Text) && !hasNumber.IsMatch(txt_lname.Text))
                {
                    conn.Open();
                    string updateLname = "UPDATE Users SET Lname = @Lname, Modify_Date = @Date WHERE ID = @ID AND Username = @Username";
                    cmd = new SqlCommand(updateLname, conn);
                    cmd.Parameters.AddWithValue("@Lname", txt_lname.Text);
                    cmd.Parameters.AddWithValue("@ID", Session["ID"]);
                    cmd.Parameters.AddWithValue("@Username", Session["username"]);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Session["lname"] = txt_lname.Text;

                    lbl_lnameMsg.Attributes.Add("style", "visibility: visible");
                    lbl_lnameMsg.Attributes.Add("style", "color: green");
                    lbl_lnameMsg.Text = "Your last name has been saved!";
                }
                else
                {
                    lbl_lnameMsg.Attributes.Add("style", "visibility: visible");
                    lbl_lnameMsg.Attributes.Add("style", "color: red");
                    lbl_lnameMsg.Text = "Your last name is not valid!";
                }
            }
           
        }

        private void UpdateFname()
        {
            if(txt_fname.Text != "")
            {
                // validate then update database record
                Regex isValidFname = new Regex("^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+");
                Regex hasNumber = new Regex(@"[0-9]+");



                if (isValidFname.IsMatch(txt_fname.Text) && !hasNumber.IsMatch(txt_fname.Text))
                {
                    conn.Open();
                    string updateFname = "UPDATE Users SET Fname = @Fname, Modify_Date = @Date WHERE ID = @ID AND Username = @Username";
                    cmd = new SqlCommand(updateFname, conn);
                    cmd.Parameters.AddWithValue("@Fname", txt_fname.Text);
                    cmd.Parameters.AddWithValue("@ID", Session["ID"]);
                    cmd.Parameters.AddWithValue("@Username", Session["username"]);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Session["fname"] = txt_fname.Text;

                    lbl_fnameMsg.Attributes.Add("style", "visibility: visible");
                    lbl_fnameMsg.Attributes.Add("style", "color: green");
                    lbl_fnameMsg.Text = "Your first name has been saved!";
                }
                else
                {
                    lbl_fnameMsg.Attributes.Add("style", "visibility: visible");
                    lbl_fnameMsg.Attributes.Add("style", "color: red");
                    lbl_fnameMsg.Text = "Your first name is not valid!";
                }
            }
      
        }

        private bool IsValidEmail()
        {
            bool result = true;
            try
            {
                string testString = txt_email.Text;
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



    }
}