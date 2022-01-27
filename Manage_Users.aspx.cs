using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Phews
{
    public partial class WebForm11 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDecision();
            }
        }

        protected void gvManageUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ArrayList arrayList = new ArrayList();
            arrayList.Add("@ID");
            arrayList.Add(Convert.ToInt32(gvManageUsers.DataKeys[e.RowIndex].Value));
            arrayList.Add("@Date");
            arrayList.Add(DateTime.Now);
            Methods.StoredProcedure("spDeleteUser", arrayList);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "K", "swal('Deleted!','User has been deleted','success')", true);
            BindDecision();
        }

        protected void gvManageUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvManageUsers.PageIndex = e.NewPageIndex;
            BindDecision();
        }

        protected void gvManageUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvManageUsers.EditIndex = e.NewEditIndex;
            BindDecision();
        }

        private void BindUsersGridView()
        {
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spDisplayUsers", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gvManageUsers.DataSource = dt;
                            gvManageUsers.DataBind();
                        }
                    }
                }
            }
        }

        private void SearchandBindUsersGridView()
        {
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spSearchUsers", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Search", txtSearch.Text);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gvManageUsers.DataSource = dt;
                            gvManageUsers.DataBind();
                        }
                    }
                }
            }
        }

        protected void gvManageUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvManageUsers.EditIndex = -1;
            BindDecision();
        }

        protected void gvManageUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            CheckBox cbA = (CheckBox)gvManageUsers.Rows[e.RowIndex].FindControl("cbAdmin");
            CheckBox cbUP = (CheckBox)gvManageUsers.Rows[e.RowIndex].FindControl("cbUserP");
            CheckBox cbUN = (CheckBox)gvManageUsers.Rows[e.RowIndex].FindControl("cbUserN");
            Label lbl = (Label)gvManageUsers.Rows[e.RowIndex].FindControl("lblID");
            if(cbA.Checked || cbUN.Checked || cbUP.Checked)

            {
                if (cbA.Checked)
                {
                    if (!RoleCheck(Convert.ToInt32(lbl.Text), 1))
                    {
                        ArrayList arrayList = new ArrayList();
                        arrayList.Add("@ID");
                        arrayList.Add(Convert.ToInt32(lbl.Text));
                        arrayList.Add("@RoleID");
                        arrayList.Add(1);
                        arrayList.Add("@Date");
                        arrayList.Add(DateTime.Now);
                        arrayList.Add("@created_By");
                        arrayList.Add("Jawad");
                        Methods.StoredProcedure("spAddRole", arrayList);
                    }
                }
                else
                {
                    ArrayList arrayList = new ArrayList();
                    arrayList.Add("@ID");
                    arrayList.Add(Convert.ToInt32(lbl.Text));
                    arrayList.Add("@RoleID");
                    arrayList.Add(1);
                    arrayList.Add("@Date");
                    arrayList.Add(DateTime.Now);
                    Methods.StoredProcedure("spDeleteRole", arrayList);
                }
                if (cbUP.Checked)
                {
                    if (!RoleCheck(Convert.ToInt32(lbl.Text), 2))
                    {
                        ArrayList arrayList = new ArrayList();
                        arrayList.Add("@ID");
                        arrayList.Add(Convert.ToInt32(lbl.Text));
                        arrayList.Add("@RoleID");
                        arrayList.Add(2);
                        arrayList.Add("@Date");
                        arrayList.Add(DateTime.Now);
                        arrayList.Add("@created_By");
                        arrayList.Add("Jawad");
                        Methods.StoredProcedure("spAddRole", arrayList);
                    }
                }
                else
                {
                    ArrayList arrayList = new ArrayList();
                    arrayList.Add("@ID");
                    arrayList.Add(Convert.ToInt32(lbl.Text));
                    arrayList.Add("@RoleID");
                    arrayList.Add(2);
                    arrayList.Add("@Date");
                    arrayList.Add(DateTime.Now);
                    Methods.StoredProcedure("spDeleteRole", arrayList);
                }
                if (cbUN.Checked)
                {
                    if (!RoleCheck(Convert.ToInt32(lbl.Text), 3))
                    {
                        ArrayList arrayList = new ArrayList();
                        arrayList.Add("@ID");
                        arrayList.Add(Convert.ToInt32(lbl.Text));
                        arrayList.Add("@RoleID");
                        arrayList.Add(3);
                        arrayList.Add("@Date");
                        arrayList.Add(DateTime.Now);
                        arrayList.Add("@created_By");
                        arrayList.Add("Jawad");
                        Methods.StoredProcedure("spAddRole", arrayList);
                    }
                }
                else
                {
                    ArrayList arrayList = new ArrayList();
                    arrayList.Add("@ID");
                    arrayList.Add(Convert.ToInt32(lbl.Text));
                    arrayList.Add("@RoleID");
                    arrayList.Add(3);
                    arrayList.Add("@Date");
                    arrayList.Add(DateTime.Now);
                    Methods.StoredProcedure("spDeleteRole", arrayList);
                }
            }
            else
            {
                
            }
        

            gvManageUsers.EditIndex = -1;
            BindDecision();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvManageUsers.EditIndex = -1;
            BindDecision();
        }

        protected void BindDecision()
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                SearchandBindUsersGridView();
            }
            else
            {
                BindUsersGridView();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            gvManageUsers.EditIndex = -1;
            txtSearch.Text = "";
            BindDecision();
        }

        protected void gvManageUsers_RowDataBound(object sender, GridViewRowEventArgs e)

        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int id = Convert.ToInt32(gvManageUsers.DataKeys[e.Row.RowIndex].Value);
                CheckBox cb = null;
                Literal ltr = null;

                //Check if the user is admin
                if (RoleCheck(id, 1))
                {
                    ltr = (Literal)e.Row.FindControl("ltrStatusAdmin");

                    if (ltr != null)
                        ltr.Text = "Yes";
                    cb = (CheckBox)e.Row.FindControl("cbAdmin");
                    if (cb != null)
                        cb.Checked = true;
                }
                //Check if the user is userP
                if (RoleCheck(id, 2))
                {
                    ltr = (Literal)e.Row.FindControl("ltrStatusUserP");

                    if (ltr != null)
                        ltr.Text = "Yes";
                    cb = (CheckBox)e.Row.FindControl("cbUserP");
                    if (cb != null)
                        cb.Checked = true;
                }
                //Check if the user is userN
                if (RoleCheck(id, 3))
                {
                    ltr = (Literal)e.Row.FindControl("ltrStatusUserN");

                    if (ltr != null)
                        ltr.Text = "Yes";
                    cb = (CheckBox)e.Row.FindControl("cbUserN");
                    if (cb != null)
                        cb.Checked = true;
                }
            }
        }

        protected bool RoleCheck(int ID, int roleID)
        {
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            SqlCommand cmd = new SqlCommand("spCheckRole", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@RoleID", roleID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}