using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Phews
{

    public partial class WebForm17 : System.Web.UI.Page
    {

        static string conString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        SqlConnection conn = new SqlConnection(conString);

        SqlCommand cmd;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindNewsCategryDropdownList();

          

                if (Session["editedArticleID"] != null)
                {
                    heading.InnerText = "Edit Article";
                    btn_post.Text = "Save Changes";

                    conn.Open();
                    string query = "SELECT * FROM News WHERE ID = @ID";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(Session["editedArticleID"]));
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txt_title.Text = reader.GetString(1);
                        txt_subtitle.Text = reader.GetString(2);
                        ddl_category.SelectedValue = reader.GetInt32(3).ToString();
                        txt_details.Text = reader.GetString(6);
                    }

                    conn.Close();


                }

            }
        }

        protected void btn_post_Click(object sender, EventArgs e)
        {


            if (!HasEmptyFields())
            {

                lbl_generalMsg.Visible = false;
                if (IsThumbnailValid())
                {
                    lbl_fileUploadMsg.Visible = false;

                    //UploadThumbnail();

                    FileInfo file = new FileInfo(file_uploadThumbnail.FileName);
                    string fname = file.Name.Remove(file.Name.Length - file.Extension.Length);
                    fname = fname + DateTime.Now.ToString("_ddMMyyhhmmss") + file.Extension;

                    file_uploadThumbnail.SaveAs(Server.MapPath("~/img/" + Path.GetFileName(fname)));

                    lbl_fileUploadMsg.Visible = true;
                    lbl_fileUploadMsg.Attributes.Add("style", "color: green");
                    lbl_fileUploadMsg.Text = "File uploaded successfully!";

                    // insert into database
                    conn.Open();

                    if (Session["editedArticleID"] == null)
                    {
                        cmd = new SqlCommand("newArticle", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Title", txt_title.Text);
                        cmd.Parameters.AddWithValue("@Subtitle", txt_subtitle.Text);
                        cmd.Parameters.AddWithValue("@Category_ID", ddl_category.SelectedValue);
                        cmd.Parameters.AddWithValue("@Img_Path", fname);
                        cmd.Parameters.AddWithValue("@User_ID", Session["ID"].ToString());
                        cmd.Parameters.AddWithValue("@Article_Text", txt_details.Text);
                        cmd.Parameters.AddWithValue("@Date", DateTime.Now.Date);
                        cmd.Parameters.AddWithValue("@Created_By", Session["username"].ToString());
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        lbl_generalMsg.Attributes.Add("style", "visibility: visible");
                        lbl_generalMsg.Attributes.Add("style", "color: green");
                        lbl_generalMsg.Text = "Article saved!";

                        lbl_generalMsg.Visible = true;
                        lbl_generalMsg.ForeColor = System.Drawing.Color.Green;
                        lbl_generalMsg.Text = "Article saved!";
                    }
                    else
                    {
                        cmd = new SqlCommand("spUpdateArticle", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ArticleID", Convert.ToInt32(Session["editedArticleID"]));
                        cmd.Parameters.AddWithValue("@Title", txt_title.Text);
                        cmd.Parameters.AddWithValue("@Subtitle", txt_subtitle.Text);
                        cmd.Parameters.AddWithValue("@Category_ID", ddl_category.SelectedValue);
                        cmd.Parameters.AddWithValue("@Img_Path", fname);
                        cmd.Parameters.AddWithValue("@User_ID", Session["ID"].ToString());
                        cmd.Parameters.AddWithValue("@Article_text", txt_details.Text);
                        cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now.Date);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        lbl_generalMsg.Attributes.Add("style", "visibility: visible");
                        lbl_generalMsg.Attributes.Add("style", "color: green");
                        lbl_generalMsg.Text = "Article saved!";

                        lbl_generalMsg.Visible = true;
                        lbl_generalMsg.ForeColor = System.Drawing.Color.Green;
                        lbl_generalMsg.Text = "Article saved!";

                        Session["editedArticleID"] = null;
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

        private bool HasEmptyFields()
        {
            if (txt_title.Text == "" || txt_subtitle.Text == "" || txt_details.Text == "" || ddl_category.SelectedValue == "" || file_uploadThumbnail.HasFile == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsThumbnailValid()
        {
            if (file_uploadThumbnail.HasFile)
            {
                string fileExtension = Path.GetExtension(file_uploadThumbnail.FileName);

                if(fileExtension.ToLower() != ".jpg" && fileExtension.ToLower().ToLower() != ".png")
                {
                    // file is invalid 
                    lbl_fileUploadMsg.Attributes.Add("style", "visibility: visible");
                    lbl_fileUploadMsg.Attributes.Add("style", "color: red");
                    lbl_fileUploadMsg.Text = "Only jpg and png files are allowed";
                    return false;
                }
                else
                {

                    int fileSize = file_uploadThumbnail.PostedFile.ContentLength;

                    if(fileSize > 5242880)
                    {
                        // file is invalid
                        lbl_fileUploadMsg.Attributes.Add("style", "visibility: visible");
                        lbl_fileUploadMsg.Attributes.Add("style", "color: red");
                        lbl_fileUploadMsg.Text = "The file exceeds 5MB!";
                        return false;
                    }
                    else
                    {
                        // file is valid
                        return true;
                    }
                }
            }
            else
            {
                // file is invalid
                lbl_fileUploadMsg.Attributes.Add("style", "visibility: visible");
                lbl_fileUploadMsg.Attributes.Add("style", "color: red");
                lbl_fileUploadMsg.Text = "File not uploaded";
                return false;
            }
        }

        private void UploadThumbnail()
        {
            FileInfo file = new FileInfo(file_uploadThumbnail.FileName);
            string fname = file.Name.Remove(file.Name.Length - file.Extension.Length);
            fname = fname + DateTime.Now.ToString("_ddMMyyhhmmss") + file.Extension;

            file_uploadThumbnail.SaveAs(Server.MapPath("~/img/" + Path.GetFileName(fname)));

            lbl_fileUploadMsg.Visible = true;
            lbl_fileUploadMsg.Attributes.Add("style", "color: green");
            lbl_fileUploadMsg.Text = "File uploaded successfully!";
        }

        private void bindNewsCategryDropdownList()
        {
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spDisplayCategories", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        //cmd.Connection = con;
                        //sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            ddl_category.DataSource = dt;
                            ddl_category.DataBind();
                        }
                    }
                }
            }
        }
    }
}