namespace Laptop.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CPU")]
    public partial class CPU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CPU()
        {
            Product = new HashSet<Product>();
        }

        [Key]
        public int CPU_Id { get; set; }

        [StringLength(50)]
        public string CPU_Name { get; set; }

        public bool? CPU_Status { get; set; }

        [StringLength(50)]
        public string CPU_ShortName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
