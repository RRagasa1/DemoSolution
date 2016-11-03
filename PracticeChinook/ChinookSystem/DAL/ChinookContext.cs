using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.Data.Entity;
using ChinookSystem.Data.Entities;
#endregion
namespace ChinookSystem.DAL
{   //internal for security reasons
    //access restricted to within the component library
    //inherit DbContext for EntityFramework 
    //     System.Data.Entity
    internal class ChinookContext:DbContext
    {
        //pass the connection string name to the
        //  DbContext using the :base() initializer
        public ChinookContext():base("ChinookDB")
        {

        }

        //setup DbSet properties
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
        public DbSet<PlayList> PlayLists { get; set; }
    }
}
