using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Phews
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected int deletedRecords = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            // ArrayList roles = new ArrayList();//1
            /// roles.Add("admin");//2
            // Session["role"] = roles;//2 - remove these 3 lines
         
            if (!IsPostBack)
            {
                BindNewsGridView();
                bindNewsCategryDropdownList();
            }
        }

        private void BindNewsGridView()
        {
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spDisplayAdminNews", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gvManageNews.DataSource = dt;
                            gvManageNews.DataBind();
                        }
                    }
                }
            }
        }

        protected void gvManageNews_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvManageNews.DataKeys[e.RowIndex].Value);
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            SqlConnection con = new SqlConnection(CS);

            SqlCommand cmd = new SqlCommand("spDeleteNews", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();
            int row = cmd.ExecuteNonQuery();
            con.Close();
            if (row > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Deleted!','News Article has been deleted','success')", true);
                bindDecision();
            }
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            gvManageNews.PageIndex = e.NewPageIndex;
            bindDecision();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindDecision();
        }

        private void searchAndBindNewsGridView()
        {
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spSearchNews", con))
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
                            gvManageNews.DataSource = dt;
                            gvManageNews.DataBind();
                        }
                    }
                }
            }
        }

        private void filterAndBindNewsGridView()
        {
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spfilterNews", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@filter", ddlCategory.SelectedValue);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gvManageNews.DataSource = dt;
                            gvManageNews.DataBind();
                        }
                    }
                }
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterAndBindNewsGridView();
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
                            ddlCategory.DataSource = dt;
                            ddlCategory.DataBind();
                        }
                    }
                }
            }
        }


        protected void gvManageNews_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int id = Convert.ToInt32(gvManageNews.DataKeys[e.NewEditIndex].Value);
            Session["editedArticleID"] = id;
            Response.Redirect("~/newArticle");
        }

        protected void bindDecision()
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                this.searchAndBindNewsGridView();
            }
            else if (ddlCategory.SelectedIndex > 0)
            {
                this.filterAndBindNewsGridView();
            }
            else
            {
                this.BindNewsGridView();
            }
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            gvManageNews.EditIndex = -1;
            txtSearch.Text = "";
            ddlCategory.SelectedIndex = 0;
            BindNewsGridView();
        }

        protected void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {

            foreach (GridViewRow gridViewRow in gvManageNews.Rows)
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
            foreach (GridViewRow gridViewRow in gvManageNews.Rows)
            {
                if (((CheckBox)gridViewRow.FindControl("cbSelect")).Checked)
                {
                    deletedRecords += 1;
                }
            }
            CheckBox headerCheckBox = (CheckBox)gvManageNews.HeaderRow.FindControl("cbSelectAll");
            if (headerCheckBox.Checked)
            {
                headerCheckBox.Checked = ((CheckBox)sender).Checked;

            }
            else
            {
                bool allCheckBoxesChecked = true;

                foreach (GridViewRow gridViewRow in gvManageNews.Rows)
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

            foreach (GridViewRow gridViewRow in gvManageNews.Rows)
            {
                if (((CheckBox)gridViewRow.FindControl("cbSelect")).Checked)
                {
                    int id = Convert.ToInt32(gvManageNews.DataKeys[gridViewRow.RowIndex].Value);
                    string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                    SqlConnection con = new SqlConnection(CS);

                    SqlCommand cmd = new SqlCommand("spDeleteNews", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    int row = cmd.ExecuteNonQuery();
                    deletedRecords += 1;
                }
            }
            if (deletedRecords > 0)
            {

                ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Deleted!','Articles have been deleted','success')", true);
                bindDecision();
            }


        }

    }
}