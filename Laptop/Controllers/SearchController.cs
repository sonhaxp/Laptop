using Laptop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laptop.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        [HttpGet]
        public ActionResult Index(string key, string manufacturer)
        {
            LaptopDBContext db = new LaptopDBContext();
            if(manufacturer.Equals("Tất cả"))
            {
                ViewBag.Product = db.Product.Where(x =>x.Product_Name.Contains(key)).ToList();
            }
            else
                ViewBag.Product = db.Product.Where(x =>x.Manufacturer.Manufacturer_Name==manufacturer&&x.Product_Name.Contains(key)).ToList();
            return View("_Product_Main", 1);
        }
    }
}