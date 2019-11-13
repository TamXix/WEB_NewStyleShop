using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewStyleShop_1.Models.ViewModel
{
    public class BillsViewModel
    {
        public Bill Bill { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
