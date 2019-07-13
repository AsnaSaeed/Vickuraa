namespace Vickuraa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("vessel")]
    public partial class vessel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public vessel()
        {
            crews = new HashSet<crew>();
            packages = new HashSet<package>();
            schedules = new HashSet<schedule>();
        }

        public int vesselID { get; set; }

        public string VesselRegNo { get; set; }

        public int ownerID { get; set; }

        public double availWeight { get; set; }

        public double availVolume { get; set; }

        [Required]
        public string vesselName { get; set; }

        public DateTime enteredDate { get; set; }

        public int enteredUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<crew> crews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<package> packages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<schedule> schedules { get; set; }

        public virtual user user { get; set; }
    }
}
