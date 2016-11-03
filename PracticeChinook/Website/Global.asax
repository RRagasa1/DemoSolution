<%@ Application Language="C#" %>
<%@ Import Namespace="Website" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="ChinookSystem.Security" %>
<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);

        //these methods are for setting up the security for the
        //execution of the application
        //this method runs when IIS starts the application
        var RoleManager = new RoleManager();
        RoleManager.AddChinookRoles();

        var UserManager = new UserManager();
        UserManager.AddWebMaster();
    }

</script>
