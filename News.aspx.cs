using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using System.Web.UI.WebControls;

namespace Phews
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        private int firstIndex, lastIndex;
        private int iPageSize = 10;

        private int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] == null)
                {
                    return 0;
                }
                return ((int)ViewState["CurrentPage"]);
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDecision();
                bindNewsCategryDropdownList();
            }
        }

        protected void BindNewsRepeater()
        {
            ArrayList arrayList = new ArrayList();
            DataTable dt = Methods.StoredProceduredt("spDisplayAdminNews", arrayList);
            PagedDataSource pdsData = new PagedDataSource();

            pdsData.DataSource = dt.DefaultView;
            pdsData.AllowPaging = true;
            pdsData.PageSize = iPageSize;
            pdsData.CurrentPageIndex = CurrentPage;
            ViewState["TotalPages"] = pdsData.PageCount;
            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + pdsData.PageCount;
            lbPrevious.Enabled = !pdsData.IsFirstPage;
            lbNext.Enabled = !pdsData.IsLastPage;
            lbFirst.Enabled = !pdsData.IsFirstPage;
            lbLast.Enabled = !pdsData.IsLastPage;
            rptrArticles.DataSource = pdsData;
            rptrArticles.DataBind();
            HandlePaging();
        }

        private void HandlePaging()
        {
            var dt = new DataTable();
            dt.Columns.Add("PageIndex"); //Start from 0
            dt.Columns.Add("PageText"); //Start from 1

            firstIndex = CurrentPage - 5;
            if (CurrentPage > 5)
                lastIndex = CurrentPage + 5;
            else
                lastIndex = 8;

            // Check last page is greater than total page then reduced it
            // to total no. of page is last index
            if (lastIndex > Convert.ToInt32(ViewState["TotalPages"]))
            {
                lastIndex = Convert.ToInt32(ViewState["TotalPages"]);
                firstIndex = lastIndex - 8;
            }

            if (firstIndex < 0)
                firstIndex = 0;

            // Now creating page number based on above first and last page index
            for (var i = firstIndex; i < lastIndex; i++)
            {
                var dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            rptPaging.DataSource = dt;
            rptPaging.DataBind();
        }

        protected void lbFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            BindDecision();
        }

        protected void lbLast_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            BindDecision();
        }

        protected void lbPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            BindDecision();
        }

        protected void lbNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            BindDecision();
        }

        protected void rptPaging_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            BindDecision();
        }

        protected void rptPaging_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            var lnkPage = (LinkButton)e.Item.FindControl("lbPaging");
            if (lnkPage.CommandArgument != CurrentPage.ToString()) return;
            lnkPage.Enabled = false;
        }

        protected void SearchAndBindNewsRepeater()
        {
            ArrayList arrayList = new ArrayList();
            arrayList.Add("@search");
            arrayList.Add(txtSearch.Text);
            DataTable dt;
            if (ddlCategory.SelectedIndex == 0)
            { dt = Methods.StoredProceduredt("spSearchNews", arrayList); }
            else
            {
                arrayList.Add("@filter");
                arrayList.Add(ddlCategory.SelectedValue);
                dt = Methods.StoredProceduredt("spSearchAndFilterNews", arrayList);
            }
            PagedDataSource pdsData = new PagedDataSource();
            pdsData.DataSource = dt.DefaultView;
            pdsData.AllowPaging = true;
            pdsData.PageSize = iPageSize;
            pdsData.CurrentPageIndex = CurrentPage;
            ViewState["TotalPages"] = pdsData.PageCount;
            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + pdsData.PageCount;
            lbPrevious.Enabled = !pdsData.IsFirstPage;
            lbNext.Enabled = !pdsData.IsLastPage;
            lbFirst.Enabled = !pdsData.IsFirstPage;
            lbLast.Enabled = !pdsData.IsLastPage;
            rptrArticles.DataSource = pdsData;
            rptrArticles.DataBind();
            HandlePaging();
        }

        protected void FilterAndBindNewsRepeater()
        {
            if (ddlCategory.SelectedIndex == 0)
            {
                BindNewsRepeater();
            }
            else
            {
                ArrayList arrayList = new ArrayList();
                arrayList.Add("@filter");
                arrayList.Add(ddlCategory.SelectedValue);
                DataTable dt;
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                { dt = Methods.StoredProceduredt("spFilterNews", arrayList); }
                else
                {
                    arrayList.Add("@search");
                    arrayList.Add(txtSearch.Text);
                    dt = Methods.StoredProceduredt("spSearchAndFilterNews", arrayList);
                }

                PagedDataSource pdsData = new PagedDataSource();
                pdsData.DataSource = dt.DefaultView;
                pdsData.AllowPaging = true;
                pdsData.PageSize = iPageSize;
                pdsData.CurrentPageIndex = CurrentPage;
                ViewState["TotalPages"] = pdsData.PageCount;
                lblpage.Text = "Page " + (CurrentPage + 1) + " of " + pdsData.PageCount;
                lbPrevious.Enabled = !pdsData.IsFirstPage;
                lbNext.Enabled = !pdsData.IsLastPage;
                lbFirst.Enabled = !pdsData.IsFirstPage;
                lbLast.Enabled = !pdsData.IsLastPage;
                rptrArticles.DataSource = pdsData;
                rptrArticles.DataBind();
                HandlePaging();
            }
        }

        protected void BindDecision()
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                this.SearchAndBindNewsRepeater();
            }
            else if (ddlCategory.SelectedIndex > 0)
            {
                this.FilterAndBindNewsRepeater();
            }
            else
            {
                this.BindNewsRepeater();
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ViewState["PageNumber"] = Convert.ToInt32(e.CommandArgument);
            BindDecision();
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterAndBindNewsRepeater();
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            ddlCategory.SelectedIndex = 0;
            BindDecision();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindDecision();
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
    }
}