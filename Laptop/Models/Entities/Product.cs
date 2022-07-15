namespace Laptop.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Cart_Detail = new HashSet<Cart_Detail>();
            Order_Detail = new HashSet<Order_Detail>();
            Purchase_Detail = new HashSet<Purchase_Detail>();
            Rate_Detail = new HashSet<Rate_Detail>();
            WishList = new HashSet<WishList>();
        }

        [StringLength(150)]
        public string Product_Name { get; set; }

        [StringLength(100)]
        public string Product_ShortName { get; set; }

        public int? CPU_Id { get; set; }

        public int? Drive_Id { get; set; }

        public int? Display_Id { get; set; }

        public int? Graphic_Id { get; set; }

        public int? RAM_Id { get; set; }

        public int? Manufacturer_Id { get; set; }

        public int? Need_Id { get; set; }

        public long? Product_Price { get; set; }

        public int? Product_Quantity { get; set; }

        public double? Product_Discount { get; set; }

        public int? Product_Status { get; set; }

        [StringLength(150)]
        public string Product_Description { get; set; }

        public double? Product_Weight { get; set; }

        [StringLength(150)]
        public string Image { get; set; }

        public int? Quantity_Sold { get; set; }

        public int? Quantity_Wish { get; set; }

        public double? Rate_Star { get; set; }

        public int? Quantity_Rate { get; set; }

        [Key]
        public int Product_Id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart_Detail> Cart_Detail { get; set; }

        public virtual CPU CPU { get; set; }

        public virtual Display Display { get; set; }

        public virtual Drive Drive { get; set; }

        public virtual Graphic Graphic { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual Need Need { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Detail> Order_Detail { get; set; }

        public virtual RAM RAM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Purchase_Detail> Purchase_Detail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rate_Detail> Rate_Detail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WishList> WishList { get; set; }
    }
}
