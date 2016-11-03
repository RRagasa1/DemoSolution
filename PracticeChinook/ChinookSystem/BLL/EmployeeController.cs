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
    public class EmployeeController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<EmployeeName> EmployeeName_Get()
        {
            using (var context = new ChinookContext())
            {
                var results = from x in context.Employees
                              orderby x.LastName, x.FirstName
                              select new EmployeeName
                              {
                                  EmployeeId = x.EmployeeId,
                                  Name = x.LastName + ", " + x.FirstName
                              };
                return results.ToList();
            }
        }
    }
}
