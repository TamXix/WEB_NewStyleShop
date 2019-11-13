using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewStyleShop_1.Data;

namespace NewStyleShop_1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminAccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AdminAccountController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.AdminAccounts.ToList());
        }

        //Get create Action Method
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewStyleShop_1.Models.AdminAccount adminaccount)
        {
            if (ModelState.IsValid)
            {
                adminaccount.CreateDate = DateTime.Now;
                _db.Add(adminaccount);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adminaccount);
        }

        //Get Edit Action Method
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var adminaccount = await _db.AdminAccounts.FindAsync(id);
            if (adminaccount == null)
            {
                return NotFound();
            }
            return View(adminaccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NewStyleShop_1.Models.AdminAccount adminaccount)
        {
            if (id != adminaccount.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                adminaccount.ModifiedDate = DateTime.Now;
                _db.Update(adminaccount);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adminaccount);
        }

        //Get Details Action Method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var adminaccount = await _db.AdminAccounts.FindAsync(id);
            if (adminaccount == null)
            {
                return NotFound();
            }
            return View(adminaccount);
        }

        //Get Delete Action Method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var adminaccount = await _db.AdminAccounts.FindAsync(id);
            if (adminaccount == null)
            {
                return NotFound();
            }
            return View(adminaccount);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adminaccount = await _db.AdminAccounts.FindAsync(id);
            _db.AdminAccounts.Remove(adminaccount);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}