using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewStyleShop_1.Data;
using NewStyleShop_1.Models.ViewModel;
namespace NewStyleShop_1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BillController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public BillsViewModel BillsVM { get; set; }
        public BillController(ApplicationDbContext db)
        {
            _db = db;
            BillsVM = new BillsViewModel()
            {
                Users = _db.Users.ToList(),
                Bill = new Models.Bill()
            };
        }
        public async Task<IActionResult> Index()
        {
            var bill = _db.Bills.Include(m => m.User);
            return View(await bill.ToListAsync());
        }

    } 
}