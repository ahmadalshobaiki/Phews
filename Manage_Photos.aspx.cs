using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Phews
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected int deletedRecords = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            // ArrayList roles = new ArrayList();//1
            /// roles.Add("admin");//2
            // Session["role"] = roles;//2 - remove these 3 lines
           
            if (!IsPostBack)
            {
                bindDecision();
                bindAlbumsDropdownList();
            }
        }

        private void BindPhotosGridView()
        {
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spDisplayPhotos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gvManagePhotos.DataSource = dt;
                            gvManagePhotos.DataBind();
                        }
                    }
                }
            }
        }

        protected void gvManagePhotos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvManagePhotos.DataKeys[e.RowIndex].Value);
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            SqlConnection con = new SqlConnection(CS);

            SqlCommand cmd = new SqlCommand("spDeletePhoto", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();
            int row = cmd.ExecuteNonQuery();
            con.Close();
            if (row > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Deleted!','Photo has been deleted','success')", true);
                bindDecision();
            }
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            gvManagePhotos.PageIndex = e.NewPageIndex;
            bindDecision();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindDecision();
        }

        private void searchAndBindPhotosGridView()
        {
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spSearchPhotos", con))
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
                            gvManagePhotos.DataSource = dt;
                            gvManagePhotos.DataBind();
                        }
                    }
                }
            }
        }

        private void filterAndBindAlbumsGridView()
        {
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spfilterPhotos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@filter", ddlAlbum.SelectedValue);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gvManagePhotos.DataSource = dt;
                            gvManagePhotos.DataBind();
                        }
                    }
                }
            }
        }

        protected void ddlAlbum_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindDecision();
        }

        private void bindAlbumsDropdownList()
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
                            ddlAlbum.DataSource = dt;
                            ddlAlbum.DataBind();
                        }
                    }
                }
            }
        }


        protected void gvManagePhotos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvManagePhotos.EditIndex = e.NewEditIndex;
            bindDecision();
        }

        protected void bindDecision()
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                this.searchAndBindPhotosGridView();
            }
            else if (ddlAlbum.SelectedIndex > 0)
            {
                this.filterAndBindAlbumsGridView();
            }
            else
            {
                this.BindPhotosGridView();
            }
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            gvManagePhotos.EditIndex = -1;
            txtSearch.Text = "";
            ddlAlbum.SelectedIndex = 0;
            bindDecision();
        }

        protected void gvManagePhotos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvManagePhotos.EditIndex = -1;
            bindDecision();
        }

        protected void gvManagePhotos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label l = gvManagePhotos.Rows[e.RowIndex].FindControl("lblID") as Label;
            TextBox tb = gvManagePhotos.Rows[e.RowIndex].FindControl("txtTitle") as TextBox;
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            SqlConnection con = new SqlConnection(CS);

            SqlCommand cmd = new SqlCommand("spUpdatePhoto", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", l.Text);
            cmd.Parameters.AddWithValue("@Title", tb.Text);
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            gvManagePhotos.EditIndex = -1;
            bindDecision();
            con.Close();
        }

        protected void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            
            foreach (GridViewRow gridViewRow in gvManagePhotos.Rows)
            {
                ((CheckBox)gridViewRow.FindControl("cbSelect")).Checked = ((CheckBox)sender).Checked;
                if (((CheckBox)sender).Checked)
                {
                    deletedRecords += 1;
                }
            }
        }

        protected void cbSelect_CheckedChanged(object sender, EventArgs e)
        {
            ((CheckBox)sender).Focus();
            foreach (GridViewRow gridViewRow in gvManagePhotos.Rows)
            {
                if (((CheckBox)gridViewRow.FindControl("cbSelect")).Checked)
                {
                    deletedRecords += 1;
                }
            }
            CheckBox headerCheckBox = (CheckBox)gvManagePhotos.HeaderRow.FindControl("cbSelectAll");
            if(headerCheckBox.Checked)
            {
                headerCheckBox.Checked = ((CheckBox)sender).Checked;
                
            }
            else
            {
                bool allCheckBoxesChecked = true;
              
                foreach (GridViewRow gridViewRow in gvManagePhotos.Rows)
                {
                    if (!((CheckBox)gridViewRow.FindControl("cbSelect")).Checked)
                    {
                        allCheckBoxesChecked = false;
                        break;
                    }
                }
                headerCheckBox.Checked = allCheckBoxesChecked;
            }
        }

        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
           
            foreach (GridViewRow gridViewRow in gvManagePhotos.Rows)
            {
                if(((CheckBox)gridViewRow.FindControl("cbSelect")).Checked)
                {
                    int id = Convert.ToInt32(((Label)gridViewRow.FindControl("lblID")).Text);
                    string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                    SqlConnection con = new SqlConnection(CS);

                    SqlCommand cmd = new SqlCommand("spDeletePhoto", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    int row = cmd.ExecuteNonQuery();
                    con.Close();
                    deletedRecords += 1;
                }
            }
            if (deletedRecords > 0)
            {
               
                ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Deleted!','Photos have been deleted','success')", true);
                bindDecision();
            }
          
          
        }
    }
}