using Laptop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laptop.Controllers
{
    public class ProductDetailController : Controller
    {
        // GET: ProductDetail
        public ActionResult Index(int id)
        {
            LaptopDBContext db = new LaptopDBContext();
            ViewBag.Product = db.Product.FirstOrDefault(x => x.Product_Id == id);
            return View();
        }
    }
}