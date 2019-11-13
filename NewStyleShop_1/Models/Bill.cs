using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewStyleShop_1.Models
{
    public class Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DeliverDate { get; set; }

        [DefaultValue(0)]
        public double TotalPrice { get; set; }

        [Required]
        [DefaultValue(0)]
        public bool Status { get; set; }

        [Display(Name = "User ID")]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

    }
}
