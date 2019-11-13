using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewStyleShop_1.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Name Product")]
        public string Name { get; set; }

        [StringLength(200)]
        public string MetaTittle { get; set; }

        [StringLength(10)]
        [Display(Name = "Code Product")]
        public string Code { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(1000)]
        [Display(Name = "Image")]
        public string Image { get; set; }

        [Display(Name = "Price")]
        [DefaultValue(0)]
        public double Price { get; set; }

        [Display(Name = "Promotion Price")]
        public double? PromotionPrice { get; set; }

        [Display(Name = "Quantity")]
        [DefaultValue(0)]
        public int? Quantity { get; set; }

        [Required]
        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Modified Date")]
        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "Male Clothes")]
        [DefaultValue(1)]
        public bool IsMale { get; set; }

        [Display(Name = "Top Hot")]
        [DefaultValue(0)]
        public bool Top { get; set; }

        [Display(Name = "Status")]
        [DefaultValue(1)]
        public bool Status { get; set; }

        [Display(Name = "Category ID")]
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
    }
}
