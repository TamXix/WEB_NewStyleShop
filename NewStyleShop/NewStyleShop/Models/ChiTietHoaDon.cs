using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewStyleShop.Models
{
    public class ChiTietHoaDon
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_HoaDon { get; set; }
        [Required]
        [Column(Order = 1)]
        public int ID_Size { get; set; }
        [Required]
        [Column(Order = 2)]
        public int ID_SanPham { get; set; }
        [Required]
        [DefaultValue(1)]
        public int Quantity { get; set; }
        [Required]
        [DefaultValue(0)]
        public double Price { get; set; }
        public HoaDon HoaDon { get; set; }
        public SanPham SanPham { get; set; }

    }
}
