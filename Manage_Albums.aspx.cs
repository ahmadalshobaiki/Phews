using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Phews
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ArrayList roles = new ArrayList();//1
            //roles.Add("admin");//2
            //Session["role"] = roles;//2 - remove these 3 lines
           
            if (!IsPostBack)
            {
                BindDecision();
            }
        }

      

        private void BindAlbumsGridView()
        {
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spDisplayAlbums", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gvManageAlbums.DataSource = dt;
                            gvManageAlbums.DataBind();
                        }
                    }
                }
            }
        }

        protected void gvManageAlbums_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int id = Convert.ToInt32(gvManageAlbums.DataKeys[e.NewEditIndex].Value);
            Session["editedAlbumID"] = id;
            Response.Redirect("~/newAlbum");
        }

        private void BindDecision()
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                this.searchAndBindAlbumsGridView();
            }
            else
            {
                this.BindAlbumsGridView();
            }
        }

        protected void gvManageAlbums_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvManageAlbums.DataKeys[e.RowIndex].Value);
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            SqlCommand cmdcheck = new SqlCommand("spAlbumIsEmpty", con);
            cmdcheck.CommandType = CommandType.StoredProcedure;
            cmdcheck.Parameters.AddWithValue("@ID", id);
            int rows = Convert.ToInt32(cmdcheck.ExecuteScalar());
            if (rows == 0)
            {
                SqlCommand cmd = new SqlCommand("spDeleteAlbum", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);

                int row = cmd.ExecuteNonQuery();
                con.Close();
                if (row > 0)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Deleted!','News Article has been deleted','success')", true);
                    BindDecision();
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Error!','Could not Delete Album All associated Photos must be deleted first ','error')", true);

                con.Close();
            }
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            gvManageAlbums.PageIndex = e.NewPageIndex;
            BindDecision();
        }

        protected void btnAddNewAlbum_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/newAlbum");
        }

        private void searchAndBindAlbumsGridView()
        {
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spSearchAlbums", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@search", txtSearch.Text);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gvManageAlbums.DataSource = dt;
                            gvManageAlbums.DataBind();
                        }
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvManageAlbums.EditIndex = -1;
            searchAndBindAlbumsGridView();
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            gvManageAlbums.EditIndex = -1;
            txtSearch.Text = "";
            BindDecision();
        }
    }
}