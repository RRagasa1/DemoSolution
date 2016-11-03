using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Microsoft.AspNet.Identity.EntityFramework;    //UserStore, ApplicationDbcontext
using Microsoft.AspNet.Identity;                    //UserManager
using System.ComponentModel;                        //ODS
using ChinookSystem.DAL;                            //Chinook Context
using ChinookSystem.Data.Entities;                  //Entities
#endregion
namespace ChinookSystem.Security
{
    [DataObject]
    public class UserManager : UserManager<ApplicationUser>
    {
        public UserManager()
            : base(new UserStore<ApplicationUser>(new ApplicationDbContext()))
        {
        }

        #region
        //the basic minimum information need for a asp.net user is
        //username, password, email
        private const string STR_WEBMASTER_USERNAME = "WebMaster";
        private const string STR_DEFAULT_PASSWORD = "Pa$$word1";
        //the {0} will be replaced us the respective username
        private const string STR_EMAIL_FORMAT = "{0}@Chinook.ab.ca";
        //the generic username will be made up of entity's firstname and lastname
        private const string STR_USERNAME_FORMAT = "{0}.{1}";
        #endregion

        //code to add a generic webmaster for the application
        public void AddWebMaster()
        {
            if (!Users.Any(u => u.UserName.Equals(STR_WEBMASTER_USERNAME)))
            {
                var webmasterAccount = new ApplicationUser()
                {
                    UserName = STR_WEBMASTER_USERNAME,
                    Email = string.Format(STR_EMAIL_FORMAT, STR_WEBMASTER_USERNAME)
                };
                //adds to the User table
                this.Create(webmasterAccount, STR_DEFAULT_PASSWORD);
                //add to appropriate role
                this.AddToRole(webmasterAccount.Id, SecurityRoles.WebsiteAdmins);
            }
        }//eom

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<UnRegisteredUserProfile> ListAllUnRegisteredUsers()
        {
            using (var context = new ChinookContext())
            {
                //get the list of registered employees
                //this will come from the AspNetUser table <ApplicationUser>
                //where the int? Employee attribute has a value
                //using .ToList() will place the results of your linq query
                //into memory.
                var registeredemployees = (from emp in Users
                                          where emp.EmployeeId.HasValue
                                          select emp.EmployeeId).ToList();
                //compare the registeredemployee list to the user table Employees
                //extract the Employees that are not registered
                var unregisteredemployees = (from emp in context.Employees
                                            where !registeredemployees.Any(eid => emp.EmployeeId == eid)
                                            select new UnRegisteredUserProfile()
                                            {
                                                CustomerEmployeeId = emp.EmployeeId,
                                                FirstName = emp.FirstName,
                                                Lastname = emp.LastName,
                                                UserType = UnregisteredUserType.Employee
                                            }).ToList();

                //get the list of registered customers
                //this will come from the AspNetUser table <ApplicationUser>
                //where the int? CustomerId attribute has a value
                var registeredcustomers = (from cus in Users
                                          where cus.CustomerId.HasValue
                                          select cus.CustomerId).ToList();
                //compare the registeredcustomer list to the user table Customer
                //extract the Customers that are not registered
                var unregisteredcustomers = (from cus in context.Customers
                                            where !registeredcustomers.Any(cid => cus.CustomerId == cid)
                                            select new UnRegisteredUserProfile()
                                            {
                                                CustomerEmployeeId = cus.CustomerId,
                                                FirstName = cus.FirstName,
                                                Lastname = cus.LastName,
                                                UserType = UnregisteredUserType.Customer
                                            }).ToList();
                //make one dataset out of the two unregistered user types
                return unregisteredemployees.Union(unregisteredcustomers).ToList();
            }
        }//eom
      
        public void RegisterUser(UnRegisteredUserProfile userinfo)
        {
            //one could randomly generate a password

            //create a new AspNetUser instance based on ApplicationUser
            var newuseraccount = new ApplicationUser()
            {
                UserName = userinfo.AssignedUserName,
                Email = userinfo.AssignedEmail
            };
            //determine and assign the user id based on type
            switch(userinfo.UserType)
            {
                case UnregisteredUserType.Customer:
                    {
                        newuseraccount.CustomerId = userinfo.CustomerEmployeeId;
                        break;
                    }
                case UnregisteredUserType.Employee:
                    {
                        newuseraccount.EmployeeId = userinfo.CustomerEmployeeId;
                        break;
                    }
            }

            //create the user account
            this.Create(newuseraccount, STR_DEFAULT_PASSWORD);

            //assign the user to a role of RegisteredUser or Staff
            switch(userinfo.UserType)
            {
                case UnregisteredUserType.Customer:
                    {
                        this.AddToRole(newuseraccount.Id, SecurityRoles.RegisteredUsers);
                        break;
                    }
                case UnregisteredUserType.Employee:
                    {
                        this.AddToRole(newuseraccount.Id, SecurityRoles.Staff);
                        break;
                    }
            }
        }//eom

        //create the UserProfile needed for the security form tab user CRUD
        //this is tied to the ODS Select
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<UserProfile> ListAllUsers()
        {
            var rm = new RoleManager();
            var results = from person in Users.ToList()
                          select new UserProfile()
                          {
                              UserId = person.Id,           //security table
                              UserName = person.UserName,   //security table
                              Email = person.Email,         //security table
                              EmailConfirmed = person.EmailConfirmed, //security table
                              CustomerId = person.CustomerId, //ApplicationUser,security table
                              EmployeeId = person.EmployeeId, //ApplicationUser, security table
                              RoleMemberships = person.Roles.Select(r => rm.FindById(r.RoleId).Name) //security tables
                          };

            //obtain the first and last name of the users
            using (var context = new ChinookContext())
            {
                Employee etemp;
                Customer ctemp;
                foreach (var person in results)
                {
                    if (person.EmployeeId.HasValue)
                    {
                        etemp = context.Employees.Find(person.EmployeeId);
                        person.FirstName = etemp.FirstName;
                        person.LastName = etemp.LastName;
                    }
                    else if(person.CustomerId.HasValue)
                    {
                        ctemp = context.Customers.Find(person.CustomerId);
                        person.FirstName = ctemp.FirstName;
                        person.LastName = ctemp.LastName;
                    }
                    else
                    {
                        person.FirstName = "Unknown";
                        person.LastName = "";
                    }
                }
            }
            return results.ToList();
        }//eom

        //this is tied to the ODS Insert command
        [DataObjectMethod(DataObjectMethodType.Insert,true)]
        public void AddUser(UserProfile userinfo)
        {
            //create an instance representing the new User 
            var useraccount = new ApplicationUser()
            {
                UserName = userinfo.UserName,
                Email = userinfo.Email
            };
            //create the new User
            this.Create(useraccount, STR_DEFAULT_PASSWORD);
            //create the UserRole(s) for the user
            foreach(var roleName in userinfo.RoleMemberships)
            {
                this.AddToRole(useraccount.Id, roleName);
            }
        }//eom

        //tied to the ODS Delete Command
        [DataObjectMethod(DataObjectMethodType.Delete,true)]
        public void RemoveUser(UserProfile userinfo)
        {
            //business logic rule
            //userinfo has the user id
            //lookup the user in the Users table
            //and obtain the username
            string UserName = this.Users.Where(u => u.Id == userinfo.UserId).Select(u => u.UserName).SingleOrDefault().ToString();
            if(UserName.Equals(STR_WEBMASTER_USERNAME))
            {
                throw new Exception("the Webmaster account cannot be removed.");
            }
            this.Delete(this.FindById(userinfo.UserId));
        }//eom
    }//eoc
}//eon
