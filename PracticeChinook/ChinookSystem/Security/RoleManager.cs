using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces 
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using ChinookSystem.Security;
using System.ComponentModel;
#endregion
namespace ChinookSystem.Security
{
    [DataObject]
    public class RoleManager:RoleManager<IdentityRole>
    {
        public RoleManager():base (new RoleStore<IdentityRole>(new ApplicationDbContext()))
        {

        }//eom

        public void AddChinookRoles()
        {
            foreach(string rolename in SecurityRoles.ChinookSecurityRoles)
            {
                //check the security tables of the Chinook system
                //to see if any new roles needs to be added to the security table data

                //the first time your application runs, all the roles
                //will be added to the security table data

                //all other times your application runs (start), only new roles
                //in the ChinookSecurityRoles will be added to the
                //security table data
                if (!Roles.Any(r => r.Name.Equals(rolename)))
                {
                    this.Create(new IdentityRole(rolename));
                }
            }
        }//eom
        
        //Read
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<RoleProfile> ListAllRoles()
        {
            var usermgr = new UserManager();

            //at points one request data to be in memory
            //for use by other queries
            //here the data from the property in RoleManager
            //needs to be brought into memory for use
            //by my linq query
            var results = from role in Roles.ToList()
                          select new RoleProfile()
                          {
                              RoleId = role.Id,
                              RoleName = role.Name,
                              UserNames = role.Users.Select(r => usermgr.FindById(r.UserId).UserName)
                          };
            return results.ToList();
        }//eom

        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<string> ListAllRoleNames()
        {
            return Roles.Select(r => r.Name).ToList();
        }
        //Add
        [DataObjectMethod(DataObjectMethodType.Insert,true)]
        public void AddRole(RoleProfile role)
        {
            //business logic validation
            //New role names cannot already exist
            if (!this.RoleExists(role.RoleName))
            {
                this.Create(new IdentityRole(role.RoleName));
            }
        }//eom
      
        //Delete
        [DataObjectMethod(DataObjectMethodType.Delete,true)]
        public void RemoveRole(RoleProfile role)
        {
            this.Delete(this.FindById(role.RoleId));
        }
    } //eoc
}//eon
