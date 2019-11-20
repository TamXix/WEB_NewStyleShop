using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewStyleShop_1.Data;
using NewStyleShop_1.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using NewStyleShop_1.Extension;
using NewStyleShop_1.Models;

namespace NewStyleShop_1.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShoppingCartController : Controller
    {

        private readonly ApplicationDbContext _db;

        [BindProperty]
        public static ShoppingCartViewModel ShoppingCartVM { get; set; }


        public ShoppingCartController(ApplicationDbContext db)
        {
            _db = db;
                
            List<int> lstShoppingCart = new List<int>();
            ShoppingCartVM = new ShoppingCartViewModel()
            {
                Products = new List<Models.Product>(),
                Appointments = new Appointments()
            };
        }

        //Get Index Shopping Cart
        public async Task<IActionResult> Index()
        {
           // IActionResult x = await Index();
            List<int> lstShoppingCart = new List<int>();
            lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");

            if (lstShoppingCart != null)
            {
                foreach(int carItem in lstShoppingCart)
                {
                    Product prod = _db.Products.Include(p => p.Category).Where(p => p.ID == carItem).FirstOrDefault();
                    ShoppingCartVM.Products.Add(prod);
                }
            }
            return View(ShoppingCartVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {

            List<int> lstCartItems = new List<int>();
            lstCartItems = HttpContext.Session.Get<List<int>>("ssShoppingCart");

            if (lstCartItems != null)
            {
                foreach (int carItem in lstCartItems)
                {
                    Product prod = _db.Products.Include(p => p.Category).Where(p => p.ID == carItem).FirstOrDefault();
                    ShoppingCartVM.Products.Add(prod);
                }
            }


            ShoppingCartVM.Appointments.AppointmentDate = ShoppingCartVM.Appointments.AppointmentDate
                            .AddHours(ShoppingCartVM.Appointments.AppointmentTime.Hour)
                            .AddMinutes(ShoppingCartVM.Appointments.AppointmentTime.Minute);

            Appointments appointments = ShoppingCartVM.Appointments;
            _db.Appointments.Add(appointments);
            _db.SaveChanges();

            int appointmentId = appointments.ID;

            foreach(int productId in lstCartItems)
            {
                ProductsSelectedForAppointment productsSelectedForAppointment = new ProductsSelectedForAppointment()
                {
                    AppointmentId = appointmentId,
                    ProductID = productId
                };
                _db.ProductsSelectedForAppointment.Add(productsSelectedForAppointment);
            }
            _db.SaveChanges();
            lstCartItems = new List<int>();
            HttpContext.Session.Set("ssShoppingCart", lstCartItems);

            //return RedirectToAction("Index");
            return RedirectToAction("AppoitmentConfirmation", "ShoppingCart", new {ID = appointmentId});
        }

        public IActionResult Remove(int id)
        {
            List<int> lstCartItems = new List<int>();
            lstCartItems = HttpContext.Session.Get<List<int>>("ssShoppingCart");

            if (lstCartItems != null)
            {
                if (lstCartItems.Contains(id))
                {
                    lstCartItems.Remove(id);
                }
            }

            HttpContext.Session.Set("ssShoppingCart", lstCartItems);
            return RedirectToAction(nameof(Index));
        }


        //Get
        public IActionResult AppoitmentConfirmation(int id)
        {
            ShoppingCartVM.Appointments = _db.Appointments.Where(a => a.ID == id).FirstOrDefault();
            List<ProductsSelectedForAppointment> objProdList = _db.ProductsSelectedForAppointment.Where(p => p.AppointmentId == id).ToList();

            foreach(ProductsSelectedForAppointment proAptObj in objProdList)
            {
                ShoppingCartVM.Products.Add(_db.Products.Include(p => p.Category).Where(p => p.ID == proAptObj.ProductID).FirstOrDefault());
            }
            return View(ShoppingCartVM);
        }
    }
}