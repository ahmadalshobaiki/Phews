using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Phews
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        private int firstIndex, lastIndex;
        private int iPageSize = 9;

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
            }
        }

        protected void BindAlbumsRepeater()
        {
            ArrayList arrayList = new ArrayList();
            DataTable dt = Methods.StoredProceduredt("spDisplayAlbums", arrayList);
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
            rptrAlbums.DataSource = pdsData;
            rptrAlbums.DataBind();
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

        protected void SearchAndBindAlbumsRepeater()
        {
            ArrayList arrayList = new ArrayList();
            arrayList.Add("@search");
            arrayList.Add(txtSearch.Text);
            DataTable dt;

            dt = Methods.StoredProceduredt("spSearchAlbums", arrayList);

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
            rptrAlbums.DataSource = pdsData;
            rptrAlbums.DataBind();
            HandlePaging();
        }

        protected void BindDecision()
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                this.SearchAndBindAlbumsRepeater();
            }
            else
            {
                this.BindAlbumsRepeater();
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ViewState["PageNumber"] = Convert.ToInt32(e.CommandArgument);
            BindDecision();
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";

            BindDecision();
        }

        protected void rptrAlbums_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int id = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "ID"));
            Literal ltrl = (Literal)e.Item.FindControl("ltrlNumOfPhotos");
            if (ltrl != null)
            {
                string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                SqlConnection con = new SqlConnection(CS);
                con.Open();
                SqlCommand cmd = new SqlCommand("spCheckPhotoNum", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    ltrl.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    ltrl.Text = "0";
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindDecision();
        }
    }
}