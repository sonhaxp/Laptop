namespace Laptop.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Drive")]
    public partial class Drive
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Drive()
        {
            Product = new HashSet<Product>();
        }

        [Key]
        public int Drive_Id { get; set; }

        [StringLength(50)]
        public string Drive_Name { get; set; }

        [StringLength(50)]
        public string Drive_ShortName { get; set; }

        public bool? Drive_Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
