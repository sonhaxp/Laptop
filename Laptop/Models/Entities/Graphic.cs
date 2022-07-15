namespace Laptop.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Graphic")]
    public partial class Graphic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Graphic()
        {
            Product = new HashSet<Product>();
        }

        [Key]
        public int Graphic_Id { get; set; }

        [StringLength(100)]
        public string Graphic_Name { get; set; }

        [StringLength(50)]
        public string Graphic_ShortName { get; set; }

        public bool? Graphic_Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
