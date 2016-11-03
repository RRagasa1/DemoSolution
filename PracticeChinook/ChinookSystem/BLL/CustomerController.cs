using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Data.Entities; //entity classes
using ChinookSystem.Data.POCOs;     //POCOs classes
using ChinookSystem.DAL;            //context class
using System.ComponentModel;        //ODS
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class CustomerController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<RepresentitiveCustomers> RepresentitiveCustomers_Get(int employeeid)
        {
            using (var context = new ChinookContext())
            {
                var results = from x in context.Customers
                              where x.SupportRepId == employeeid
                              orderby x.LastName, x.FirstName
                              select new RepresentitiveCustomers
                              {
                                  Name = x.LastName + ", " + x.FirstName,
                                  City = x.City,
                                  State = x.State,
                                  Phone = x.Phone,
                                  Email = x.Email
                              };
                return results.ToList();

            }
        }
    }
}
