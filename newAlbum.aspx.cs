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
    public partial class WebForm16 : System.Web.UI.Page
    {

        static string conString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        SqlConnection conn = new SqlConnection(conString);

        SqlCommand cmd;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               

                if (Session["editedAlbumID"] != null)
                {
                    heading.InnerText = "Edit Album";
                    btn_submit.Text = "Save Changes";

                    conn.Open();
                    string query = "SELECT * FROM Album WHERE ID = @ID";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(Session["editedAlbumID"]));
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txt_title.Text = reader.GetString(1);
                        txt_desc.Text = reader.GetString(2);
                    }

                    conn.Close();


                }
            }

        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            // validate the form then remove editedalbum session

            if (!HasEmptyFields())
            {

                //lbl_generalMsg.Visible = false;
                if (IsThumbnailValid())
                {
                    //lbl_fileUploadMsg.Visible = false;

                    //UploadThumbnail();

                    FileInfo file = new FileInfo(file_uploader.FileName);
                    string fname = file.Name.Remove(file.Name.Length - file.Extension.Length);
                    fname = fname + DateTime.Now.ToString("_ddMMyyhhmmss") + file.Extension;

                    file_uploader.SaveAs(Server.MapPath("~/img/" + Path.GetFileName(fname)));

                    // insert into database
                    conn.Open();

                    if (Session["editedAlbumID"] == null)
                    {
                        cmd = new SqlCommand("spNewAlbum", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Title", txt_title.Text);
                        cmd.Parameters.AddWithValue("@Description", txt_desc.Text);
                        cmd.Parameters.AddWithValue("@Thumbnail", fname);
                        cmd.Parameters.AddWithValue("@Date", DateTime.Now.Date);
                        cmd.Parameters.AddWithValue("@User_ID", Session["ID"].ToString());
                        cmd.Parameters.AddWithValue("@Created_By", Session["username"].ToString());
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        lbl_generalMsg.Attributes.Add("style", "visibility: visible");
                        lbl_generalMsg.Attributes.Add("style", "color: green");
                        lbl_generalMsg.Text = "Album saved!";
                    }
                    else
                    {
                        string title = txt_title.Text;
                        string desc = txt_desc.Text;
                        cmd = new SqlCommand("spUpdateAlbum", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AlbumID", Convert.ToInt32(Session["editedAlbumID"]));
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@Description", desc);
                        cmd.Parameters.AddWithValue("@Thumbnail", fname);
                        cmd.Parameters.AddWithValue("@User_ID", Session["ID"].ToString());
                        cmd.Parameters.AddWithValue("@ModifyDate", DateTime.Now.Date);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        lbl_generalMsg.Attributes.Add("style", "visibility: visible");
                        lbl_generalMsg.Attributes.Add("style", "color: green");
                        lbl_generalMsg.Text = "Album saved!";

                        Session["editedAlbumID"] = null;
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
            if (txt_title.Text == "" || txt_desc.Text == "" || file_uploader.HasFile == false)
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
            if (file_uploader.HasFile)
            {
                string fileExtension = Path.GetExtension(file_uploader.FileName);

                if (fileExtension.ToLower() != ".jpg" && fileExtension.ToLower().ToLower() != ".png")
                {
                    // file is invalid 
                    lbl_fileUploadMsg.Attributes.Add("style", "visibility: visible");
                    lbl_fileUploadMsg.Attributes.Add("style", "color: red");
                    lbl_fileUploadMsg.Text = "Only jpg and png files are allowed";
                    return false;
                }
                else
                {

                    int fileSize = file_uploader.PostedFile.ContentLength;

                    if (fileSize > 5242880)
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
            FileInfo file = new FileInfo(file_uploader.FileName);
            string fname = file.Name.Remove(file.Name.Length - file.Extension.Length);
            fname = fname + DateTime.Now.ToString("_ddMMyyhhmmss") + file.Extension;

            file_uploader.SaveAs(Server.MapPath("~/img/" + Path.GetFileName(fname)));

            
        }

    }
}