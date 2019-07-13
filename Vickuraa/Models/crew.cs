namespace Vickuraa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("crew")]
    public partial class crew
    {
        public int crewID { get; set; }

        public int userID { get; set; }

        public int vesselID { get; set; }

        public int rankId { get; set; }

        [Required]
        public string crewName { get; set; }

        public int crewContactNo { get; set; }

        public double salary { get; set; }

        public virtual crewRank crewRank { get; set; }

        public virtual user user { get; set; }

        public virtual vessel vessel { get; set; }
    }
}
