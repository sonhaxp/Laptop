namespace Laptop.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Provider")]
    public partial class Provider
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Provider()
        {
            Purchase = new HashSet<Purchase>();
        }

        [Key]
        public int Provider_Id { get; set; }

        [StringLength(50)]
        public string Provider_Name { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        public long? PhoneNumber { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        public bool? Provider_Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Purchase> Purchase { get; set; }
    }
}
