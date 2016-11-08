namespace EToolsSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vendor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vendor()
        {
            PurchaseOrders = new HashSet<PurchaseOrder>();
            StockItems = new HashSet<StockItem>();
        }

        public int VendorID { get; set; }

        [Required]
        [StringLength(100)]
        public string VendorName { get; set; }

        [Required]
        [StringLength(12)]
        public string Phone { get; set; }

        [Required]
        [StringLength(30)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(2)]
        public string ProvinceID { get; set; }

        [Required]
        [StringLength(6)]
        public string PostalCode { get; set; }

        public virtual Province Province { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockItem> StockItems { get; set; }
    }
}
