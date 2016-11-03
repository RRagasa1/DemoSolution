using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region Additional Namespaces
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace ChinookSystem.Data.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }

        //by default the EntityFramework navigation
        //expects foreign keys to have the same name as the 
        //parent primary key
        //if not you MUST use the ForeignKey annotation to
        //relate the foreign key field to the appropriate
        //navigation property using the property name
        [ForeignKey("Employee")]
        public int? SupportRepId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}

