using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewStyleShop.Data;
using NewStyleShop.Models;

namespace NewStyleShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Admins.ToList());
        }

        //Get create Action Method
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewStyleShop.Models.Admin admin)
        {
            if(ModelState.IsValid)
            {
                _db.Add(admin);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        //

        //Get Edit Action Method
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var admin = await _db.Admins.FindAsync(id);
            if(admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NewStyleShop.Models.Admin admin)
        {
            if(id!=admin.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Update(admin);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }


        //

        //Get Details Action Method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var admin = await _db.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }


        //


        //Get Delete Action Method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var admin = await _db.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = await _db.Admins.FindAsync(id);
            _db.Admins.Remove(admin);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


    }
}