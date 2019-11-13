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

       
    }
}