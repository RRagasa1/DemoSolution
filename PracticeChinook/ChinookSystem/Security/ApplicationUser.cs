using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespace
using Microsoft.AspNet.Identity.EntityFramework;
#endregion
namespace ChinookSystem.Security
{
    public class ApplicationUser : IdentityUser
    {
        public int? EmployeeId { get; set; }
        public int? CustomerId { get; set; }
    }
}
