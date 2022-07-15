namespace Laptop.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RAM")]
    public partial class RAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RAM()
        {
            Product = new HashSet<Product>();
        }

        [Key]
        public int RAM_Id { get; set; }

        [StringLength(50)]
        public string RAM_Name { get; set; }

        [StringLength(20)]
        public string RAM_ShortName { get; set; }

        public bool? RAM_Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
