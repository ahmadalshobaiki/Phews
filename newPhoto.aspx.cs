using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;

namespace Phews
{
    /* 
     * TODO:
     * 1- CHANGE USER INTERFACE TO ALLOW APPLICATION OF CODE LOGIC - DONE
     * 2- ON UPLOAD CLICK, UPLOAD TO FILE SYSTEM AND DATABASE - DONE
     * 3- DISPLAY UPLOADED IMAGES THUMBNAIL AND EDIT TITLE
     * 
     * 
     * 2- JQUERY - TO CREATE UPLOAD INTERFACE - not possible
     *  1- TEMPORARY TABLE SP -> BUT FIRST CHECK TEMPDB IF TEMPTABLE EXISTS OR NOT. IF YES DROP IT IF_OBJECTID(TEMPDB.."TABLENAME") 
     *  
     *  
     *  
     *  
     *  1- give images unique name - done
     *  2- upload to the file system  - done
     *  3- store in a string variable with delimiter - done
     *  4 - pass string builder and make script to separate the img names from string builder - done
     *  5- insert to temp table - done
     *  6- then insert to actual table - done
     *  7- display on repeater
     *  
     *  
     *  or
     *  
     *  upload to database first with unique filenames then make select query where @imgnames = stringbuilder.tostring and store it into temptable
     *  then display image previews to the user from temptables
     *  
     *  
     *  insert into temptable -> insert into main table one by one then SCOPE_IDENTITY() -> insert into temp table again with primary key
     *  in the sql script
     */

    public partial class WebForm18 : System.Web.UI.Page
    {        

        static string conString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        SqlConnection conn = new SqlConnection(conString);

        SqlCommand cmd;

        protected void Page_Load(object sender, EventArgs e)
        {
           

            if (!IsPostBack)
            {
                BindAlbumDropDownList();
                
            }
        }

        protected void btn_upload_Click(object sender, EventArgs e)
        {
            /* 1- validate the pictures before uploading to file system and database
               2- the pictures will be displayed to the user after uploading, and the user can edit their captions,
               3- once the user clicks save, the pictures will have their names changed. 
            */

            StringBuilder sb = new StringBuilder();

            if (!HasEmptyFields())
            {
                lbl_generalMsg.Attributes.Add("style", "visibility: hidden");

                HttpFileCollection httpFileCollection = Request.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    HttpPostedFile httpPostedFile = httpFileCollection[i];
                    if (httpPostedFile.ContentLength > 0)
                    {
                        // validate the image here before uploading and saving 
                        string fileExtension = Path.GetExtension(httpPostedFile.FileName);
                        if (fileExtension.ToLower() != ".jpg" && fileExtension.ToLower().ToLower() != ".png")
                        {
                            // invalid file extension
                            lbl_photosInfo.Attributes.Add("style", "visibility: visible");
                            lbl_photosInfo.Attributes.Add("style", "color: red");
                            lbl_photosInfo.Text = "Only jpg and png files are allowed";
                        }
                        else
                        {

                            int fileSize = httpPostedFile.ContentLength;

                            if (fileSize > 5242880)
                            {
                                // file size is invalid
                                lbl_photosInfo.Attributes.Add("style", "visibility: visible");
                                lbl_photosInfo.Attributes.Add("style", "color: red");
                                lbl_photosInfo.Text = "The file exceeds 5MB!";
                            }
                            else
                            {
                                // file is valid -> upload with a unique filename
                                FileInfo file = new FileInfo(httpPostedFile.FileName);
                                string fname = file.Name.Remove(file.Name.Length - file.Extension.Length);
                                fname = fname + DateTime.Now.ToString("_ddMMyyhhmmss") + file.Extension;

                                httpPostedFile.SaveAs(Server.MapPath("~/img/" + Path.GetFileName(fname)));
                                lbl_photosInfo.Attributes.Add("style", "visibility: visible");
                                lbl_photosInfo.Attributes.Add("style", "color: green");
                                lbl_photosInfo.Text = "Files have been uploaded successfully!";

                                sb.Append(fname + "$");


                                //// insert to temptable 
                                //// create a script to store to temptable
                                //conn.Open();
                                //cmd = new SqlCommand("spPhotoTempTable", conn);
                                //cmd.CommandType = CommandType.StoredProcedure;
                                //cmd.Parameters.AddWithValue("@Title", fname);
                                //cmd.Parameters.AddWithValue("@Img_Path", ("~/img/" + fname));
                                //cmd.ExecuteNonQuery();


                                //// save to database 
                                //conn.Open();
                                //cmd = new SqlCommand("spNewPhoto", conn);
                                //cmd.CommandType = CommandType.StoredProcedure;
                                //cmd.Parameters.AddWithValue("@Title",fname);
                                //cmd.Parameters.AddWithValue("@Img_Path", ("~/img/" + fname));
                                //cmd.Parameters.AddWithValue("@User_ID", Session["ID"].ToString());
                                //cmd.Parameters.AddWithValue("@Date", DateTime.Now.Date);
                                //cmd.Parameters.AddWithValue("Created_By", Session["username"].ToString());
                                //cmd.Parameters.AddWithValue("@Album_ID", ddl_album.SelectedValue);
                                //cmd.ExecuteNonQuery();
                                //conn.Close();


                                //// display on repeater from temptable
                                //SqlDataReader reader = cmd.ExecuteReader();
                                //rptr_photos.DataSource = reader;
                                //rptr_photos.DataBind();
                                //conn.Close();
                            }
                        }
                    }
                }

                conn.Open();
                cmd = new SqlCommand("spSplitString", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@String", sb.ToString());
                cmd.Parameters.AddWithValue("@Separator", "$");
                cmd.Parameters.AddWithValue("@User_ID", Session["ID"].ToString());
                cmd.Parameters.AddWithValue("@Date", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@Created_By", Session["username"].ToString());
                cmd.Parameters.AddWithValue("@Album_ID", ddl_album.SelectedValue);
                
                //cmd.ExecuteNonQuery();

                //SqlDataReader reader = cmd.ExecuteReader();
                //rptr_photos.DataSource = reader;
                //rptr_photos.DataBind();
                //reader.Close();

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                int rowCount = dataTable.Rows.Count;
                sqlDataAdapter.Fill(dataTable);
                rptr_photos.DataSource = dataTable;
                rptr_photos.DataBind();
                conn.Close();



                // save to temptable
                //string images = sb.ToString();
                //String[] imgList = images.Split('$');
                //conn.Open();
                //cmd = new SqlCommand("spPhotoTempTable", conn);
                //cmd.CommandType = CommandType.StoredProcedure;
                //foreach (String img in imgList)
                //{
                //    cmd.Parameters.AddWithValue("@Title", img);
                //    cmd.Parameters.AddWithValue("@Img_Path", ("~/img/" + img));
                //    cmd.ExecuteNonQuery();
                //}
                //conn.Close();

            }
            else
            {
                lbl_generalMsg.Attributes.Add("style", "visibility: visible");
                lbl_generalMsg.Attributes.Add("style", "color: red");
                lbl_generalMsg.Text = "One or more fields are empty!";
            }

            

        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            // on click, update the name of the images that were changed. 

            //RepeaterItem repeaterItem = (sender as Button).NamingContainer as RepeaterItem;

            //string title = (repeaterItem.FindControl("txt_imgTitle") as TextBox).Text;

            conn.Open();
            int rowCount = rptr_photos.Items.Count - 1;
            foreach (RepeaterItem item in rptr_photos.Items)
            {
                
                string newTitle = (item.FindControl("txt_imgTitle") as TextBox).Text;
                string imgPath = (item.FindControl("lbl_title") as Label).Text;
                if (newTitle != null)
                {
                    cmd = new SqlCommand("spUpdateImgTitle", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@newTitle", newTitle);
                    cmd.Parameters.AddWithValue("@Img_Path", imgPath);
                    cmd.Parameters.AddWithValue("@i", rowCount);
                    

                    cmd.ExecuteNonQuery();
                    rowCount--;
                }
                
                
            }

            conn.Close();

        }

        private void BindAlbumDropDownList()
        {
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spDisplayAlbums", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        //cmd.Connection = con;
                        //sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            ddl_album.DataSource = dt;
                            ddl_album.DataBind();
                        }
                    }
                }
            }
        }

        private bool HasEmptyFields()
        {
            if (ddl_album.SelectedValue == "" || file_uploadPhotos.HasFile == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}