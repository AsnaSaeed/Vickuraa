namespace Vickuraa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schedule")]
    public partial class schedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public schedule()
        {
            bookings = new HashSet<booking>();
        }

        public int scheduleID { get; set; }

        public int routeID { get; set; }

        public int vesselID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime estimateDeparturetime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime estimateArrivaltime { get; set; }

        public DateTime? arrivedDate { get; set; }

        public DateTime? departedDate { get; set; }

        public int enteredUser { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime entereddate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<booking> bookings { get; set; }

        public virtual route route { get; set; }

        public virtual user user { get; set; }

        public virtual vessel vessel { get; set; }
    }
}
