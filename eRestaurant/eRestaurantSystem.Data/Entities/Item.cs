using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additonal Namespaces
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace eRestaurantSystem.Data.Entities
{
    [Table("Items")]
    public class Item
    {
        [Key]
        public int ItemID { get; set; }
        public string Description { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal CurrentCost { get; set; }
        public bool Active { get; set; }
        public int? Calories { get; set; }
        public string Comments { get; set; }
        public int MenuCategoryID { get; set; }

        public virtual MenuCategory MenuCategory { get; set; }
        public virtual ICollection<BillItem> BillItems { get; set; }
    }
}
