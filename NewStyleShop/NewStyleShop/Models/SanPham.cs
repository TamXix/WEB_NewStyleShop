using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewStyleShop.Models
{
    public class SanPham
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(200)]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }

        [StringLength(200)]
        public string MetaTittle { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã sản phẩm")]
        public string Code { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(200)]
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        [Display(Name = "Giá sản phẩm")]
        [DefaultValue(0)]
        public double Price { get; set; }

        [Display(Name = "Giá khuyến mãi")]
        public double? PromotionPrice { get; set; }

        [Display(Name = "Số lượng")]
        public int? Quantity { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Ngày sửa")]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "Đồ nam")]
        [DefaultValue(1)]
        public int Gender { get; set; }

        [Display(Name = "Top hot")]
        [DefaultValue(1)]
        public int? Top { get; set; }

        [Display(Name = "Trạng thái")]

        [DefaultValue(1)]
        public int Status { get; set; }

        [Display(Name = "Danh mục sản phẩm")]

        public int ID_DanhMucSP { get; set; }

        public virtual DanhMucSP DanhMucSP { get; set; }

        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDon { get; set; }

    }
}
