using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewStyleShop.Models
{
    public class DanhMucSP
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Danh mục")]
        public string Name { get; set; }

        [Required]
        public string MetaTitle { get; set; }

        [Required]
        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Display(Name = "Ngày sửa")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [Display(Name = "Trạng thái")]
        public int Status { get; set; }

        public virtual ICollection<SanPham> SanPham { get; set; }

    }
}
