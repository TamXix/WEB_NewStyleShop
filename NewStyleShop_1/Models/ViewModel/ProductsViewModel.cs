using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewStyleShop_1.Models.ViewModel
{
    public class ProductsViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
