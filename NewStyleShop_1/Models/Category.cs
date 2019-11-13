using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewStyleShop_1.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Catelogy")]
        public string Name { get; set; }

        public string MetaTitle { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]

        public DateTime CreatedDate { get; set; }
        //public DateTime DateCreated
        //{
        //    get
        //    {
        //        return this.dateCreated.HasValue
        //           ? this.dateCreated.Value
        //           : DateTime.Now;
        //    }

        //    set { this.dateCreated = value; }
        //}

        //private DateTime? dateCreated = null;


        [DataType(DataType.Date)]
        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        [Required]
        [Display(Name = "Status")]
        public bool Status { get; set; }


    }
}
