using System;

namespace Phews
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HandleUsers();
            activateNavbarLink();
        }

        private void activateNavbarLink()
        {
            string thisURL = Request.Url.AbsolutePath;
            if (thisURL.Equals("/Home_Admin", StringComparison.CurrentCultureIgnoreCase))
            {
                adminlHomeNavbar.Attributes.Add("class", "nav-item active");
            }
            else if (thisURL.Equals("/Manage_Albums", StringComparison.CurrentCultureIgnoreCase))
            {
                managePhotosNavbar.Attributes.Add("class", "nav-item dropdown active");
            }
            else if (thisURL.Equals("/Manage_Photos", StringComparison.CurrentCultureIgnoreCase))
            {
                managePhotosNavbar.Attributes.Add("class", "nav-item dropdown active");
            }
            else if (thisURL.Equals("/Manage_News", StringComparison.CurrentCultureIgnoreCase))
            {
                ManageNewsNavbar.Attributes.Add("class", "nav-item dropdown active");
            }
            else if (thisURL.Equals("/Manage_News_Categories", StringComparison.CurrentCultureIgnoreCase))
            {
                ManageNewsNavbar.Attributes.Add("class", "nav-item dropdown active");
            }
            else if (thisURL.Equals("/Manage_Users", StringComparison.CurrentCultureIgnoreCase))
            {
                manageUsersNavbar.Attributes.Add("class", "nav-item active");
            }
        }

        private void HandleUsers()
        {
            if (Methods.isLoggedIn())
            {
                if (Methods.isAdmin() || Methods.isSuperAdmin())
                {
                    if (!Methods.isSuperAdmin())
                    {
                        manageUsersNavbar.Visible = false;
                        string thisURL = Request.Url.AbsolutePath;
                        if (thisURL.Equals("/Manage_Users", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Response.Redirect("/Home_Admin");
                        }
                    }
                }
                else
                {
                    Response.Redirect("/Home_General");
                }
            }
            else
            {
                Response.Redirect("/signup");
            }
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("signup.aspx");
        }
    }
}