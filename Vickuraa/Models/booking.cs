namespace Vickuraa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("booking")]
    public partial class booking
    {
        public int bookingID { get; set; }

        public int scheduleID { get; set; }

        public int? customerID { get; set; }

        public int quantity { get; set; }

        [Required]
        public string receiverName { get; set; }

        [Required]
        public string receiverAddress { get; set; }

        public int receiverContactNo { get; set; }

        [Required]
        public string senderName { get; set; }

        public int senderContactNo { get; set; }

        public int packageID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime date { get; set; }

        public int invoiceNo { get; set; }

        public int invoiceYear { get; set; }

        public int? RecievedPhysicalUser { get; set; }

        public DateTime? RecievedPhyscialDate { get; set; }

        public int? paymentRecievedUser { get; set; }

        public DateTime? paymentRecievedDate { get; set; }

        public int? receiptNo { get; set; }

        public int? receiptYear { get; set; }

        public int? dispatchedUser { get; set; }

        public DateTime? dispatchedDate { get; set; }

        public virtual package package { get; set; }

        public virtual schedule schedule { get; set; }

        public virtual user user { get; set; }
    }
}
