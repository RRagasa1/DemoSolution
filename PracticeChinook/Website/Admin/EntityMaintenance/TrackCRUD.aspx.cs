using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespace
#endregion

public partial class Admin_EntityMaintenance_TrackCRUD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //this method is the method to direct the handling ODS expections

    //Ensure you have add the assemblies reference System.Data.Entry to
    //your website project

    //to install on an ODS
    //switch the web page view to Design
    //select the required ODS (TrackListODS) and open properties
    //select the event icon
    //select OnInserted, OnUpdated, OnDeleted and select method from list
    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }
 
}