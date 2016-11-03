using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additonal Namespaces
using eRestaurantSystem.Data.Entities;
using System.Data.Entity;
#endregion

namespace eRestaurantSystem.DAL
{
    internal class eRestaurantContext:DbContext
    {
        public eRestaurantContext():base("eRestaurantDB")
        {

        }

        public DbSet<Item> Items { get; set; }
        public DbSet<BillItem> BillItems { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<MenuCategory> MenuCategories { get; set; }
    }
}
