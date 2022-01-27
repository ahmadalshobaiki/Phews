using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Phews
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblcategoryNameErr.Visible = false;

            //ArrayList roles = new ArrayList();//1
            //roles.Add("admin");//2
            //Session["role"] = roles;//2 - remove these 3 lines
        
            if (!IsPostBack)
            {
                BindNewsCategoriesGridView();
            }
        }


        private void BindNewsCategoriesGridView()
        {
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spDisplayCategories", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gvManageNewsCategories.DataSource = dt;
                            gvManageNewsCategories.DataBind();
                        }
                    }
                }
            }
        }

        protected void gvManageNewsCategories_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvManageNewsCategories.EditIndex = e.NewEditIndex;
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                this.searchAndBindNewsCategoryGridView();
            }
            else
            {
                this.BindNewsCategoriesGridView();
            }
        }

        protected void gvManageNewsCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvManageNewsCategories.DataKeys[e.RowIndex].Value);
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            SqlCommand cmdcheck = new SqlCommand("spCategoryIsEmpty", con);
            cmdcheck.CommandType = CommandType.StoredProcedure;
            cmdcheck.Parameters.AddWithValue("@ID", id);
            int rows = Convert.ToInt32(cmdcheck.ExecuteScalar());
            if (rows == 0)
            {
                SqlCommand cmd = new SqlCommand("spDeleteNewsCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);

                int row = cmd.ExecuteNonQuery();
                con.Close();
                if (row > 0)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Deleted!','News Article has been deleted','success')", true);
                    if (!string.IsNullOrEmpty(txtSearch.Text))
                    {
                        this.searchAndBindNewsCategoryGridView();
                    }
                    else
                    {
                        this.BindNewsCategoriesGridView();
                    }
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Error!','Could not Delete Category All associated Articles must be deleted first ','error')", true);

                con.Close();
            }
        }

        protected void gvManageNewsCategories_RowCancelingEditing(object sender, GridViewCancelEditEventArgs e)
        {
            lblcategoryNameErr.Visible = false;
            gvManageNewsCategories.EditIndex = -1;
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                this.searchAndBindNewsCategoryGridView();
            }
            else
            {
                this.BindNewsCategoriesGridView();
            }
        }

        protected void gvManageNewsCategories_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label l = gvManageNewsCategories.Rows[e.RowIndex].FindControl("lblID") as Label;
            TextBox tb = gvManageNewsCategories.Rows[e.RowIndex].FindControl("txtName") as TextBox;
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            SqlConnection con = new SqlConnection(CS);

            SqlCommand cmd = new SqlCommand("spUpdateNewsCategory", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", l.Text);
            cmd.Parameters.AddWithValue("@Name", tb.Text);
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            SqlCommand cmdcheck = new SqlCommand("spCategoryNameExists", con);
            cmdcheck.CommandType = CommandType.StoredProcedure;
            cmdcheck.Parameters.AddWithValue("@Name", tb.Text);
            con.Open();

            int rows = Convert.ToInt32(cmdcheck.ExecuteScalar());
            if (rows == 0)
            {
                lblcategoryNameErr.Visible = false;
                cmd.ExecuteNonQuery();
                con.Close();

                gvManageNewsCategories.EditIndex = -1;
                BindNewsCategoriesGridView();
            }
            else
            {
                lblcategoryNameErr.Visible = true;
            }
            con.Close();
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            gvManageNewsCategories.PageIndex = e.NewPageIndex;
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                this.searchAndBindNewsCategoryGridView();
            }
            else
            {
                this.BindNewsCategoriesGridView();
            }
        }

        protected void btnAddNewCategory_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            string s = txtNewCategory.Text;
            txtNewCategory.Focus();

            if (!string.IsNullOrWhiteSpace(s))
            {
                txtNewCategory.Text = "";
                string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                SqlConnection con = new SqlConnection(CS);
                con.Open();
                SqlCommand cmdGetUsername = new SqlCommand("Select username from Users Where ID = " + Session["ID"], con);
                SqlDataAdapter sda = new SqlDataAdapter(cmdGetUsername);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                string username = dt.Rows[0][0].ToString();
                SqlCommand cmdcheck = new SqlCommand("spCategoryNameExists", con);
                cmdcheck.CommandType = CommandType.StoredProcedure;
                cmdcheck.Parameters.AddWithValue("@Name", s);
                SqlCommand cmd = new SqlCommand("spAddCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", s);
                cmd.Parameters.AddWithValue("@Record_Date", DateTime.Now);
                cmd.Parameters.AddWithValue("@Modify_Date", DateTime.Now);
                cmd.Parameters.AddWithValue("@Created_By", username);
                // con.Close();
                //  con.Open();

                int rows = Convert.ToInt32(cmdcheck.ExecuteScalar());
                if (rows == 0)
                {
                    lblcategoryNameErr.Visible = false;
                    lblcategoryAddNameErr.Visible = false;

                    cmd.ExecuteNonQuery();
                    con.Close();

                    gvManageNewsCategories.EditIndex = -1;
                    BindNewsCategoriesGridView();
                }
                else
                {
                    lblcategoryAddNameErr.InnerText = "The category Name already exists";
                    lblcategoryAddNameErr.Visible = true;
                }
                con.Close();
            }
            else
            {
                lblcategoryAddNameErr.InnerText = "The category Name Can't be empty";
                lblcategoryAddNameErr.Visible = true;
            }
        }

        private void searchAndBindNewsCategoryGridView()
        {
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spSearchCategories", con))
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
                            gvManageNewsCategories.DataSource = dt;
                            gvManageNewsCategories.DataBind();
                        }
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvManageNewsCategories.EditIndex = -1;
            searchAndBindNewsCategoryGridView();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            lblcategoryNameErr.Visible = false;
            lblcategoryAddNameErr.Visible = false;
            txtNewCategory.Text = "";
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            gvManageNewsCategories.EditIndex = -1;
            txtSearch.Text = "";
            BindNewsCategoriesGridView();
        }
    }
}