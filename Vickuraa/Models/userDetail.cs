namespace Vickuraa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("userDetail")]
    public partial class userDetail
    {
        public int userdetailID { get; set; }


        public string Name { get; set; }


        public string Email { get; set; }

    
        public string ContactNo { get; set; }

        public int? userID { get; set; }

      
        public string ContactNo2 { get; set; }

        public virtual user user { get; set; }
    }
}
