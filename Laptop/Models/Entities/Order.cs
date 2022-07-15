namespace Laptop.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            Order_Detail = new HashSet<Order_Detail>();
        }

        [Key]
        public int Order_Id { get; set; }

        public int? User_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Order_Date { get; set; }

        public int? Order_Status { get; set; }

        public long? Total { get; set; }

        public int? Seller_Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Detail> Order_Detail { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }
    }
}
