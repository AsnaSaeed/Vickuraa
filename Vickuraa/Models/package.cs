namespace Vickuraa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("package")]
    public partial class package
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public package()
        {
            bookings = new HashSet<booking>();
        }

        public int packageID { get; set; }

        public int packageTypeId { get; set; }

        public int vesselID { get; set; }

        public double volume { get; set; }

        public double weight { get; set; }

        public double price { get; set; }

        public DateTime enteredDate { get; set; }

        public int enteredUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<booking> bookings { get; set; }

        public virtual packageType packageType { get; set; }

        public virtual vessel vessel { get; set; }
    }
}
