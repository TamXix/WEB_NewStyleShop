using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewStyleShop.Data;

namespace NewStyleShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DanhMucController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DanhMucController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.DanhMucSPs.ToList());
        }

        //Get create Action Method
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewStyleShop.Models.DanhMucSP danhMucSP)
        {
            if (ModelState.IsValid)
            {
                _db.Add(danhMucSP);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(danhMucSP);
        }

        //Get Edit Action Method
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var danhMuc = await _db.DanhMucSPs.FindAsync(id);
            if (danhMuc == null)
            {
                return NotFound();
            }
            return View(danhMuc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NewStyleShop.Models.DanhMucSP danhMucSP)
        {
            if (id != danhMucSP.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Update(danhMucSP);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(danhMucSP);
        }

        //Get Details Action Method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var danhmuc = await _db.DanhMucSPs.FindAsync(id);
            if (danhmuc == null)
            {
                return NotFound();
            }
            return View(danhmuc);
        }

        //Get Delete Action Method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var danhmuc = await _db.DanhMucSPs.FindAsync(id);
            if (danhmuc == null)
            {
                return NotFound();
            }
            return View(danhmuc);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var danhmuc = await _db.DanhMucSPs.FindAsync(id);
            _db.DanhMucSPs.Remove(danhmuc);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}