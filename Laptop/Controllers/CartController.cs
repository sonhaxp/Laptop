using Laptop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laptop.Controllers
{
    public class CartController : Controller
    {
        LaptopDBContext db = new LaptopDBContext();
        public List<Cart_Detail> GetCart()
        {
            List<Cart_Detail> carts = (List<Cart_Detail>)Session["cart"];
            return carts == null ? new List<Cart_Detail>() : carts;
        }

        public ActionResult Index()
        {
            ViewBag.Cart = GetCart();
            return View();
        }
        public ActionResult AddCart(int id)
        {
            Product product = db.Product.FirstOrDefault(x => x.Product_Id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart_Detail> cart_session = GetCart();
            List<Cart_Detail> cart = null;
            User user = (User)Session["user"];
            if (user != null)
            {
                cart = db.Cart_Detail.Where(x => x.User_Id == user.User_Id).ToList();
            }
            Cart_Detail cart_Detail = cart_session.Find(x => x.Product_Id == id);
            if (cart_Detail == null)
            {
                cart_Detail = new Cart_Detail();
                cart_Detail.Product_Id = id;
                cart_Detail.Quantity = 1;
                if (user != null)
                {
                    cart_Detail.User_Id = user.User_Id;
                    db.Cart_Detail.Add(cart_Detail);
                }
                cart_session.Add(cart_Detail);
            }
            else
            {
                if (user != null)
                {
                    Cart_Detail cart_1 = cart.Find(x => x.Product_Id == id);
                    cart_1.User_Id = user.User_Id;
                    cart_1.Quantity++;
                }
                cart_Detail.Quantity++;
            }
            db.SaveChanges();
            return Redirect("/gio-hang");
        }
        [HttpPut]
        public ActionResult UpdateCart(int id, int quantity)
        {
            Product product = db.Product.FirstOrDefault(x => x.Product_Id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart_Detail> carts = GetCart();
            List<Cart_Detail> cart = null;
            User user = (User)Session["user"];
            if (user != null)
            {
                cart = db.Cart_Detail.Where(x => x.User_Id == user.User_Id).ToList();
            }
            Cart_Detail cart_Detail = carts.Find(x => x.Product_Id == id);
            if (cart_Detail != null)
            {
                int quality = quantity;
                if (quality > 0)
                {
                    if (user != null)
                    {
                        Cart_Detail cart_Detail1 = cart.Find(x => x.Product_Id == id);
                        cart_Detail1.Quantity = quantity;
                        db.SaveChanges();
                    }
                    cart_Detail.Quantity = quantity;
                }
                else
                {
                    if (user != null)
                    {
                        Cart_Detail cart_Detail1 = cart.Find(x => x.Product_Id == id);
                        db.Cart_Detail.Remove(cart_Detail1);
                        db.SaveChanges();
                    }
                    carts.Remove(cart_Detail);
                }
                
            }
            ViewBag.Cart = carts;
            return PartialView("_Cart");
        }
        [HttpDelete]
        public ActionResult DeleteCart(int id)
        {
            Product product = db.Product.FirstOrDefault(x => x.Product_Id == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart_Detail> carts = GetCart();
            List<Cart_Detail> cart = null;
            User user = (User)Session["user"];
            if (user != null)
            {
                cart = db.Cart_Detail.Where(x => x.User_Id == user.User_Id).ToList();
            }
            Cart_Detail cart_Detail = carts.Find(x => x.Product_Id == id);
            if (cart_Detail != null)
            {
                if (user != null)
                {
                    Cart_Detail cart_Detail1 = cart.Find(x => x.Product_Id == id);
                    db.Cart_Detail.Remove(cart_Detail1);
                }
                carts.Remove(cart_Detail);
            }
            db.SaveChanges();
            ViewBag.Cart = carts;
            return PartialView("_Cart");
        }
        [HttpPost]
        public ActionResult GetCountCart()
        {
            return Json(new
            {
                message = GetCart().Count
            });
        }
        public ActionResult Checkout()
        {
            ViewBag.Cart = GetCart();
            ViewBag.user = (User)Session["user"];
            if (GetCart().Count == 0) return Redirect("/trang-chu");
            if (Session["user"] == null) return Redirect("/dang-nhap");
            return View();
        }
        public long getTotal()
        {
            long total = 0;
            foreach (var i in GetCart())
            {
                total = total + (long)(i.Quantity * i.Product.Product_Price * (1 - i.Product.Product_Discount / 100));
            }
            return total;
        }
        [HttpPost]
        public ActionResult Checkout_Order(string name, string email, string phone, string address)
        {
            User user = (User)Session["user"];
            Order order = new Order();
            order.User_Id = user.User_Id;
            order.Order_Date = DateTime.Now;
            order.Order_Status = 0;
            order.Total = getTotal();
            order.Name = name;
            order.Email = email;
            order.PhoneNumber = phone;
            order.Address = address;
            try 
            {
                db.Order.Add(order);
                db.SaveChanges();
                foreach(var i in GetCart())
                {
                    Order_Detail order_Detail = new Order_Detail();
                    order_Detail.Oder_Id = order.Order_Id;
                    order_Detail.Product_Id = i.Product_Id;
                    order_Detail.Quantity = i.Quantity;
                    order_Detail.Price = i.Product.Product_Price;
                    order_Detail.Discount = i.Product.Product_Discount;
                    order_Detail.Total = (long?)(order_Detail.Price * (1-order_Detail.Discount/100));
                    db.Order_Detail.Add(order_Detail);
                }
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    db.Order_Detail.RemoveRange(db.Order_Detail.Where(x => x.Oder_Id == order.Order_Id));
                    db.Order.Remove(order);
                    return Json(new
                    {
                        message = "Error"
                    });
                }
            }
            catch
            {
                return Json(new
                {
                    message = "Error"
                });
            }
            db.Cart_Detail.RemoveRange(db.Cart_Detail.Where(x=>x.User_Id==user.User_Id));
            db.SaveChanges();
            Session["cart"] = null;
            return Json(new
            {
                message = "Success"
            });
        }
    }
}