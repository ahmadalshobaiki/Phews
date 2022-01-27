using System;

namespace Phews
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["Role"] !=null)
            {
                if (!Methods.isUserN())
                {
                    pNews.InnerHtml = "<button class='btn btn-secondary' disabled>Go to News</button>";
             
                }
                if (!Methods.isUserP())
                {
                    pPhotos.InnerHtml = "<button class='btn btn-secondary' disabled>Go to News</button>";
               
                }
            }
     
        }
    }
}