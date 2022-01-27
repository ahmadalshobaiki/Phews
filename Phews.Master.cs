using System;

namespace Phews
{
    public partial class Phews : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HandleUsers();
            activateNavbarLink();
        }

        private void activateNavbarLink()
        {
            string thisURL = Request.Url.AbsolutePath;
            if (thisURL.Equals("/Home_General", StringComparison.CurrentCultureIgnoreCase))
            {
                generalHomeNavbar.Attributes.Add("class", "nav-item active");
            }
            else if (thisURL.Equals("/Albums", StringComparison.CurrentCultureIgnoreCase))
            {
                photosNavbar.Attributes.Add("class", "nav-item active");
            }
            else if (thisURL.Equals("/photos", StringComparison.CurrentCultureIgnoreCase))
            {
                photosNavbar.Attributes.Add("class", "nav-item active");
            }
            else if (thisURL.Equals("/About_us", StringComparison.CurrentCultureIgnoreCase))
            {
                aboutUsNavbar.Attributes.Add("class", "nav-item active");
            }
            else if (thisURL.Equals("/News", StringComparison.CurrentCultureIgnoreCase))
            {
                NewsNavbar.Attributes.Add("class", "nav-item active");
            }
        }

        private void HandleUsers()
        {
            if (!Methods.isLoggedIn())
            {
                Response.Redirect("/signup");
            }
            else if (!Methods.isUserN() && !Methods.isUserP())
            {
                Response.Redirect("/Home_Admin");
            }
            else if (!Methods.isUserN())
            {
                NewsNavbar.Visible = false;
                string thisURL = Request.Url.AbsolutePath;
                if (thisURL.Equals("/News", StringComparison.CurrentCultureIgnoreCase))
                {
                    Response.Redirect("/Home_General");
                }
            }
            else if (!Methods.isUserP())
            {
                photosNavbar.Visible = false;
                string thisURL = Request.Url.AbsolutePath;
                if (thisURL.Equals("/Albums", StringComparison.CurrentCultureIgnoreCase) || thisURL.Equals("/Photos", StringComparison.CurrentCultureIgnoreCase))
                {
                    Response.Redirect("/Home_General");
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("signup.aspx");
        }
    }
}