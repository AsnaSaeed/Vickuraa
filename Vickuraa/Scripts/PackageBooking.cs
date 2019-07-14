using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Vickuraa.Models;

namespace Vickuraa.Dto
{
 
    public class PackageBooking
    {
        [Key]
        public int packageID { get; set; }
        public string packageDesc { get; set; }
        public int packageTypeId { get; set; }

        public int vesselID { get; set; }

        public int volume { get; set; }

        public double price { get; set; }

        public DateTime enteredDate { get; set; }

        public int enteredUser { get; set; }

        public int scheduleID { get; set; }

        public int? customerID { get; set; }


        [Required]
        public string receiverName { get; set; }

        [Required]
        public string receiverAddress { get; set; }

        public int receiverContactNo { get; set; }

        public string senderName { get; set; }

        public int senderContactNo { get; set; }

   

        [Column(TypeName = "datetime2")]
        public DateTime date { get; set; }
        public int Quantity { get; set; }
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


        public virtual schedule schedule { get; set; }

        public virtual user user { get; set; }
    }
}