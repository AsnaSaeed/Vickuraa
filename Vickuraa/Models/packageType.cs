namespace Vickuraa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("packageType")]
    public partial class packageType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public packageType()
        {
            packages = new HashSet<package>();
        }

        public int packageTypeId { get; set; }

        [Column("packageType")]
        [Required]
        public string packageType1 { get; set; }

        public double Rate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<package> packages { get; set; }
    }
}
