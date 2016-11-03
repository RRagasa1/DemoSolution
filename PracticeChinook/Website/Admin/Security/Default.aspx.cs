using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using ChinookSystem.Security;
#endregion

public partial class Admin_Security_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void RefreshAll(object sender, EventArgs e)
    {
        DataBind();
    }

    protected void UnregisteredUsersGridView_SelectedIndexChanging(object sender, 
                                                            GridViewSelectEventArgs e)
    {
        //position the gridview to the row that was selected
        UnregisteredUsersGridView.SelectedIndex = e.NewSelectedIndex;
        //create shortcut variable that physically points to the selected row
        GridViewRow agvrow = UnregisteredUsersGridView.SelectedRow;
        //do we have a row
        if (agvrow != null)
        {
            //access the text box fields and retreive their contents
            //use the method .FindControl("controlidname") as controltype
            //this will point to the control
            //then you can access the control using the control's access method
            //data of user controls are normally strings
            string username = (agvrow.FindControl("AssignedUserName") as TextBox).Text;
            string email = (agvrow.FindControl("AssignedEmail") as TextBox).Text;

            //load these 2 fields and the rest of the gridview row data into a
            //UnRegisteredUserProfile instance

            //create and fill a new instance of an object
            UnRegisteredUserProfile user = new UnRegisteredUserProfile()
            {
                CustomerEmployeeId = int.Parse(UnregisteredUsersGridView.SelectedDataKey.Value.ToString()), //originally an object
                UserType = (UnregisteredUserType)Enum.Parse(typeof(UnregisteredUserType),
                            agvrow.Cells[1].Text),  //.Cells[index] points to a display only column
                FirstName = agvrow.Cells[2].Text,
                Lastname = agvrow.Cells[3].Text,
                AssignedUserName = username,
                AssignedEmail = email
            };

            //register the user
            UserManager sysmgr = new UserManager();
            sysmgr.RegisterUser(user);
            DataBind();
        }
    }

    protected void UserListView_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        //one needs to walk through the checkboxlist
        //create the RoleMembership string list of select roles
        var addtoroles = new List<string>();
        //point to the phyiscal checkboxlist control
        var roles = e.Item.FindControl("RoleMemberships") as CheckBoxList;
        //does it exist
        if(roles != null)
        {
            //cycle through the checkbox list
            //find which roles have been selected
            //add to the List<string>
            foreach(ListItem role in roles.Items)
            {
                if (role.Selected)
                {
                    addtoroles.Add(role.Value);
                }
                e.Values["RoleMemberships"] = addtoroles;
            }
            //store the username
           
        }
    }

}