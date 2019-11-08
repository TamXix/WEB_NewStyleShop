using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewStyleShop.Models
{
    public class HoaDon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime DeliverDate { get; set; }
        [Required]
        public bool Status { get; set; }


        public int ID_User { get; set; }
        public User User { get; set; }

        public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

    }
}
