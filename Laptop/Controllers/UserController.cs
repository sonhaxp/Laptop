using Laptop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laptop.Controllers
{
    public class UserController : Controller
    {
        LaptopDBContext db = new LaptopDBContext();
        LaptopDBContext1 db1 = new LaptopDBContext1();
        public ActionResult Info()
        {
            User user = (User)Session["user"];
            ViewBag.user = user;
            ViewBag.chitieu = db.Order.Where(x => x.User_Id==user.User_Id&&x.Order_Date.Value.Year == DateTime.Now.Year&&x.Order_Status==1).Sum(x => x.Total);
            if (ViewBag.chitieu == null)
            {
                ViewBag.chitieu = 0;
            }
            DateTime datefrom = DateTime.Now.AddMonths(-1);
            DateTime dateto = DateTime.Now;
            List<Order> orders = db.Order.Where(x => x.User_Id == user.User_Id && x.Order_Date >= datefrom && x.Order_Date <= dateto).OrderByDescending(x => x.Order_Date).ToList();
            ViewBag.Order = orders;
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public JsonResult checkLogin(FormCollection collection)
        {
            string username = collection["email"];
            string password = collection["password"];
            JsonResult jr = new JsonResult();
            User u = db.User.FirstOrDefault(x=>x.Email==username);
            if (u == null || u.Role.Role_Name != "user")
            {
                jr.Data = new
                {
                    status = "F"
                };
            }
            else
            {
                if (u.Password.Trim() == GetMD5(password))
                {
                    List<Cart_Detail> carts = db.Cart_Detail.Where(x => x.User_Id == u.User_Id).ToList();
                    List<Cart_Detail> carts_session = (List<Cart_Detail>)Session["cart"];
                    List<WishList> wish = db.WishList.Where(x => x.User_Id == u.User_Id).ToList();
                    List<WishList> wish_session = (List<WishList>)Session["wishlist"];
                    foreach (var i in carts_session)
                    {
                        Cart_Detail cart_Detail = carts.Find(x => x.Product_Id == i.Product_Id);
                        if (cart_Detail == null)
                        {
                            cart_Detail = new Cart_Detail();
                            cart_Detail.Product_Id = i.Product_Id;
                            cart_Detail.User_Id = u.User_Id;
                            cart_Detail.Quantity = i.Quantity;
                            cart_Detail.Product = db.Product.FirstOrDefault(x => x.Product_Id == i.Product_Id);
                            cart_Detail.User = u;
                            carts.Add(cart_Detail);
                            db.Cart_Detail.Add(cart_Detail);
                        }
                        else
                        {
                            cart_Detail.Quantity+= i.Quantity;
                        }  
                    }
                    int t = db.SaveChanges();
                    foreach (var i in wish_session)
                    {
                        WishList wishList = wish.Find(x => x.Product_Id == i.Product_Id);
                        if (wishList == null)
                        {
                            wishList = new WishList();
                            wishList.Product_Id = i.Product_Id;
                            wishList.User_Id = u.User_Id;
                            wishList.Status = true;
                            wish.Add(wishList);
                            db.WishList.Add(wishList);
                        }
                    }
                    db.SaveChanges();
                    Session["cart"] = carts;
                    Session["user"] = u;
                    Session["wishlist"] = wish;
                    Session.Timeout = 45;
                    jr.Data = new
                    {
                        status = "OK"
                    };
                }
                else
                {
                    jr.Data = new
                    {
                        status = "F"
                    };
                }
            }
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Logout()
        {
            Session.Remove("user");
            return Redirect("/trang-chu");
        }
        [HttpPost]
        public ActionResult Register_User(string name,string password, string email,DateTime birthday, string gender,string address,string phoneNumber)
        {
            JsonResult jr = new JsonResult();
            User u = db.User.FirstOrDefault(x=>x.Email==email);
            if (u == null)
            {
                u = new User();
                u.Name = name;
                u.Password = GetMD5(password);
                u.Email = email;
                u.Birthday = birthday;
                u.Gender = gender;
                u.Address = address;
                u.PhoneNumber = phoneNumber;
                u.UserStatus = true;
                u.Role_Id = 2;
                try
                {
                    db.User.Add(u);
                    db.SaveChanges();
                    return Json(new
                    {
                        message = "OK"
                    });
                }
                catch (Exception)
                {
                    Response.StatusCode = 404;
                    return null;
                }
            }
            else
            {
                return Json(new
                {
                    message = "exist_email"
                });
            }
            
        }
        [HttpGet]
        public ActionResult Order(DateTime datefrom,DateTime dateto)
        {
            User user = (User)Session["user"];
            List<Order> orders = db.Order.Where(x =>x.User_Id==user.User_Id && x.Order_Date >= datefrom && x.Order_Date <= dateto).ToList();
            orders = orders.OrderByDescending(x => x.Order_Id).ToList();
            ViewBag.Order = orders;
            return PartialView("_Model_Transaction");
        }
        [HttpPut]
        public ActionResult ChangeInfo(string address,string oldpass,string newpass)
        {
            User user = (User)Session["user"];
            user.Address = address;
            if (oldpass != "" && newpass != "")
            {
                if (GetMD5(oldpass).Equals(user.Password))
                {
                    user.Password = GetMD5(newpass);
                }
                else
                {
                    return Json(new
                    {
                        message = "error"
                    });
                }
            }
            try
            {
                db.SaveChanges();
                return Json(new
                {
                    message = "ok"
                });
            }
            catch
            {
                return Json(new
                    {
                        message = "error"
                    });
            }
        }

        [HttpGet]
        public ActionResult GetOrderDetail(int order_id)
        {
            ViewBag.order = db.Order_Detail.Where(x => x.Oder_Id == order_id).ToList();
            return PartialView("_Order_Modal");
        }
        private String GetMD5(string txt)
        {
            String str = "";
            Byte[] buffer = System.Text.Encoding.UTF8.GetBytes(txt);
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            foreach (Byte b in buffer)
            {
                str += b.ToString("X2");
            }
            return str;
        }
    }
}