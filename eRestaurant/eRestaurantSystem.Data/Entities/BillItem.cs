using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace eRestaurantSystem.Data.Entities
{
    [Table("BillItems")]
    public class BillItem
    {
        [Key, Column(Order = 1)]
        public int BillID { get; set; }
        [Key, Column(Order = 2)]
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal UnitCost { get; set; }
        public string Notes { get; set; }
        public virtual Item Item { get; set; }
        public virtual Bill Bill { get; set; }

    }
}
