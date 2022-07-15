using Laptop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laptop.Controllers
{
    public class WishListController : Controller
    {
        LaptopDBContext db = new LaptopDBContext();
        [HttpGet]
        public List<WishList> GetWishList()
        {
            List<WishList> wishLists = (List<WishList>)Session["wishlist"];
            return wishLists == null ? new List<WishList>() : wishLists;
        }
        [HttpGet]
        public ActionResult GetWish()
        {
            List<WishList> wishLists = (List<WishList>)Session["wishlist"];
            wishLists = wishLists == null ? new List<WishList>() : wishLists;
            List<int> id = wishLists.Select(x=>x.Product_Id).ToList();
            return Json(id, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            ViewBag.WishList = GetWishList();
            return View();
        }
        [HttpPost]
        public ActionResult AddorDeleteWishList(int id)
        {
            Product product = db.Product.FirstOrDefault(x => x.Product_Id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<WishList> wishlist_session = GetWishList();
            List<WishList> wishlist = null;
            User user = (User)Session["user"];
            if (user != null)
            {
                wishlist = db.WishList.Where(x => x.User_Id == user.User_Id).ToList();
            }
            WishList wishList = wishlist_session.Find(x => x.Product_Id == id);
            if (wishList == null)
            {
                wishList = new WishList();
                wishList.Product_Id = id;
                if (user != null)
                {
                    wishList.User_Id = user.User_Id;
                    db.WishList.Add(wishList);
                    db.SaveChanges();
                }
                wishlist_session.Add(wishList);
                return Json(new
                {
                    message = "add"
                });
            }
            else
            {
                if (user != null)
                {
                    WishList wishList1 = wishlist.Find(x => x.Product_Id == id);
                    db.WishList.Remove(wishList1);
                    db.SaveChanges();
                }
                wishlist_session.Remove(wishList);
            }
            return Json(new
            {
                message = "delete"
            });
        }
        [HttpDelete]
        public ActionResult DeleteWishList(int id)
        {
            Product product = db.Product.FirstOrDefault(x => x.Product_Id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<WishList> wishlist_session = GetWishList();
            List<WishList> wishlist = null;
            User user = (User)Session["user"];
            if (user != null)
            {
                wishlist = db.WishList.Where(x => x.User_Id == user.User_Id).ToList();
            }
            WishList wishList = wishlist_session.Find(x => x.Product_Id == id);
            if (wishList != null)
            {
                if (user != null)
                {
                    WishList wishList1 = wishlist.Find(x => x.Product_Id == id);
                    db.WishList.Remove(wishList1);
                }
                wishlist_session.Remove(wishList);
            }
            db.SaveChanges();
            ViewBag.WishList = wishlist_session;
            return PartialView("_WishList");
        }
        [HttpPost]
        public ActionResult GetCountWishList()
        {
            return Json(new
            {
                message = GetWishList().Count
            }) ;
        }
    }
}