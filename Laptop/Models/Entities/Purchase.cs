namespace Laptop.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Purchase")]
    public partial class Purchase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Purchase()
        {
            Purchase_Detail = new HashSet<Purchase_Detail>();
        }

        [Key]
        public int Purchase_Id { get; set; }

        public int? User_Id { get; set; }

        public int? Provider_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Purchase_Date { get; set; }

        public bool? Purchase_Status { get; set; }

        public long? Total { get; set; }

        public virtual Provider Provider { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Purchase_Detail> Purchase_Detail { get; set; }
    }
}
