using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.UI;
using Website;

#region Additional Namespaces for Security
using ChinookSystem.Security;
#endregion
public partial class Account_Register : Page
{
    protected void CreateUser_Click(object sender, EventArgs e)
    {
        var manager = new UserManager();
        var user = new ApplicationUser() { UserName = UserName.Text };

        //creates the user record on AspNetUsers
        IdentityResult result = manager.Create(user, Password.Text);
        if (result.Succeeded)
        {
            //if a person successfully registers they will be treated
            //as a security role RegisteredUser
            manager.AddToRole(user.Id, SecurityRoles.RegisteredUsers);

            IdentityHelper.SignIn(manager, user, isPersistent: false);
            IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
        }
        else
        {
            ErrorMessage.Text = result.Errors.FirstOrDefault();
        }
    }
}