using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewStyleShop_1.Models
{
    public class BillDetail
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [DefaultValue(1)]
        public int Quantity { get; set; }

        [Required]
        [DefaultValue(0)]
        public double Price { get; set; }


        [Display(Name = "Bill ID")]
        public int BillID { get; set; }

        [ForeignKey("BillID")]
        public virtual Bill Bill { get; set; }


        [Display(Name = "Product ID")]
        public int ProductID { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}
