using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using NewStyleShop_1.Data;
using NewStyleShop_1.Models;
using NewStyleShop_1.Models.ViewModel;
using NewStyleShop_1.Utility;

namespace NewStyleShop_1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostingEnvironment;
        [BindProperty]
        public ProductsViewModel productVM { get; set; }
        public ProductController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            productVM = new ProductsViewModel()
            {
                Categories = _db.Categories.ToList(),
                Product = new Models.Product()
            };
        }
        public async Task<IActionResult> Index()
        {
            var product = _db.Products.Include(m => m.Category);
            return View(await product.ToListAsync());
        }

        //Get : Product Create
        public IActionResult Create()
        {
            return View(productVM);
        }
        //Post : Product Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            if (!ModelState.IsValid)
            {
                return View(productVM);
            }
            productVM.Product.CreatedDate = DateTime.Now;
            _db.Products.Add(productVM.Product);
            await _db.SaveChangesAsync();

            //Image being saved
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            var productsFromDb = _db.Products.Find(productVM.Product.ID);

            if (files.Count != 0)
            {
                //Image has been uploaded
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                var extension = Path.GetExtension(files[0].FileName);

                using (var filestream = new FileStream(Path.Combine(uploads, productVM.Product.ID + extension), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }
                productsFromDb.Image = @"\" + SD.ImageFolder + @"\" + productVM.Product.ID + extension;

            }
            else
            {
                //when user does not upload image
                var uploads = Path.Combine(webRootPath, SD.ImageFolder + @"\" + SD.DefaultProductImage);
                System.IO.File.Copy(uploads, webRootPath + @"\" + SD.ImageFolder + @"\" + productVM.Product.ID + ".jpg");
                productsFromDb.Image = @"\" + SD.ImageFolder + @"\" + productVM.Product.ID + ".jpg";

            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //GET : Edit 
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            productVM.Product = await _db.Products.Include(m => m.Category).SingleOrDefaultAsync(m => m.ID == id);
            if (productVM.Product == null)
            {
                return NotFound();
            }
            return View(productVM);
        }

        // Post : Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                var productFromDb = _db.Products.Where(m => m.ID == productVM.Product.ID).FirstOrDefault();

                if (files.Count > 0 && files[0] != null)
                {
                    //if user uploads a new image
                    var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                    var extension_new = Path.GetExtension(files[0].FileName);
                    var extension_old = Path.GetExtension(productFromDb.Image);
                    if (System.IO.File.Exists(Path.Combine(uploads, productVM.Product.ID + extension_old)))
                    {
                        System.IO.File.Delete(Path.Combine(uploads, productVM.Product.ID + extension_old));
                    }
                    using (var filestream = new FileStream(Path.Combine(uploads, productVM.Product.ID + extension_new), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    productVM.Product.Image = @"\" + SD.ImageFolder + @"\" + productVM.Product.ID + extension_new;

                }
                if (productVM.Product.Image != null)
                {
                    productFromDb.Image = productVM.Product.Image;
                }
                productFromDb.Name = productVM.Product.Name;
                productFromDb.MetaTittle = productVM.Product.MetaTittle;
                productFromDb.Code = productVM.Product.Code;
                productFromDb.Description = productVM.Product.Description;
                productFromDb.Image = productVM.Product.Image;
                productFromDb.Price = productVM.Product.Price;
                productFromDb.PromotionPrice = productVM.Product.PromotionPrice;
                productFromDb.Quantity = productVM.Product.Quantity;
                productFromDb.CreatedDate = productVM.Product.CreatedDate;
                productFromDb.ModifiedDate = productVM.Product.ModifiedDate;
                productFromDb.IsMale = productVM.Product.IsMale;
                productFromDb.Top = productVM.Product.Top;
                productFromDb.Status = productVM.Product.Status;
                productFromDb.CategoryID = productVM.Product.CategoryID;
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(productVM);
        }

        //Get : Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            productVM.Product = await _db.Products.Include(m => m.Category).SingleOrDefaultAsync(m => m.ID == id);
            if (productVM.Product == null)
            {
                return NotFound();
            }
            return View(productVM);
        }

        //Get : Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            productVM.Product = await _db.Products.Include(m => m.Category).SingleOrDefaultAsync(m => m.ID == id);
            if (productVM.Product == null)
            {
                return NotFound();
            }
            return View(productVM);
        }

        //Post : Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            Product product = await _db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                var extension = Path.GetExtension(product.Image);
                if (System.IO.File.Exists(Path.Combine(uploads, product.ID + extension)))
                {
                    System.IO.File.Delete(Path.Combine(uploads, product.ID + extension));

                }
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
    }
}