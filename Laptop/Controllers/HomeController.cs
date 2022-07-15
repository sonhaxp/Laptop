using Laptop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laptop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new List<Cart_Detail>();
                Session.Timeout = 45;
            }
            if (Session["wishlist"] == null)
            {
                Session["wishlist"] = new List<WishList>();
                Session.Timeout = 45;
            }
            return View();
        }
        public ActionResult RenderManufacturer()
        {
            LaptopDBContext db = new LaptopDBContext();
            ViewBag.Manufacturer = db.Manufacturer.Where(x => x.Status_Id == true).ToList();
            if (Session["cart"] == null)
            {
                Session["cart"] = new List<Cart_Detail>();
                Session.Timeout = 45;
            }
            if (Session["wishlist"] == null)
            {
                Session["wishlist"] = new List<WishList>();
                Session.Timeout = 45;
            }
            ViewBag.cart = ((List<Cart_Detail>)Session["cart"]).Count();
            ViewBag.wishlist = ((List<WishList>)Session["wishlist"]).Count();
            ViewBag.user = Session["user"];
            return View("_Header");
        }
        public ActionResult RenderProduct()
        {
            LaptopDBContext db = new LaptopDBContext();
            ViewBag.BestSeller = db.Product.OrderByDescending(x => x.Quantity_Sold).Take(4).ToList();
            ViewBag.BestDiscount = db.Product.OrderByDescending(x => x.Product_Discount).Take(3).ToList();
            ViewBag.BestWish = db.Product.OrderByDescending(x => x.Quantity_Wish).Take(4).ToList();
            return View("_Main");
        }
    }
}