using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using ChinookSystem.Security;
#endregion
public partial class Queries_FirstSample : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        { 
            ReleaseYear.Text = DateTime.Today.Year.ToString();
        }
        //restrict this page to logged in users
        if (!Page.IsPostBack)
        {
            //are you logged in Authentication
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                //do you have authorization to be on this page
                //role checking
                if (!User.IsInRole(SecurityRoles.WebsiteAdmins))
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }
    }
}