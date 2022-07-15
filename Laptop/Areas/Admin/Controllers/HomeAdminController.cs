using Laptop.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laptop.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin

        public ActionResult Index()
        {
            User admin = (User)Session["admin"];
            ViewBag.admin = admin;
            if (admin == null) return View("_Login");
            return View();
        }
        public ActionResult checkLogin(string username,string password)
        {
            LaptopDBContext db = new LaptopDBContext();
            User u = db.User.FirstOrDefault(x => x.Email == username);
            if (u == null)
            {
                return Json(new
                {
                    message = "F"
                },JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (u.Password.Trim() == GetMD5(password) && u.Role_Id == 1)
                {
                    Session["admin"] = u;
                    return Json(new
                    {
                        message = "OK"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        message = "F"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult TrangChu()
        {
            return PartialView("_Content");
        }
        public ActionResult Logout()
        {
            Session["admin"] = null;
            return RedirectToRoute("admin");
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
        public ActionResult Order()
        {
            return PartialView("_Order");
        }
        [HttpPut]
        public ActionResult UpdateOrder(int id, int status)
        {
            LaptopDBContext db = new LaptopDBContext();
            Order order = db.Order.FirstOrDefault(x => x.Order_Id == id);
            order.Order_Status = status;
            order.Seller_Id = ((User)Session["admin"]).User_Id;
            if(status==2)
                foreach(var i in db.Order_Detail.Where(x => x.Oder_Id == order.Order_Id))
                {
                    Product product = db.Product.FirstOrDefault(x => x.Product_Id == i.Product_Id);
                    product.Product_Quantity += i.Quantity;
                    product.Quantity_Sold -= i.Quantity;
                }
            db.SaveChanges();
            
            return Json(new
            {
                status = "ok"
            });
        }
        public ActionResult Product()
        {
            LaptopDBContext db = new LaptopDBContext();
            ViewBag.CPU = db.CPU.Where(x=>x.CPU_Status==true).ToList();
            ViewBag.Drive = db.Drive.Where(x => x.Drive_Status == true).ToList();
            ViewBag.Display = db.Display.Where(x => x.Display_Status == true).ToList();
            ViewBag.Graphic = db.Graphic.Where(x => x.Graphic_Status == true).ToList();
            ViewBag.RAM = db.RAM.Where(x => x.RAM_Status == true).ToList();
            ViewBag.Manufacturer = db.Manufacturer.Where(x => x.Status_Id == true).ToList();
            ViewBag.Need = db.Need.Where(x => x.Need_Status == true).ToList();
            return PartialView("_Product");
        }
        [HttpPost]
        public ActionResult UploadAndUpdateProduct(int id,string productname, string productshortname, int cpu, int drive, int display, int graphic, int ram, int manufacturer, int need, int status, HttpPostedFileBase image, string detail, double weight,int quantity,int discount, int per, long price)
        {
            LaptopDBContext db = new LaptopDBContext();
            Product product;
            if (per == 0)
            {
                product = new Product();
                product.Rate_Star = 0;
                product.Quantity_Rate = 0;
                product.Quantity_Sold = 0;
                product.Quantity_Wish = 0;
            }
            else
            {
                product = db.Product.FirstOrDefault(x => x.Product_Id == id);
            }
            product.Product_Name = productname;
            product.Product_ShortName = productshortname;
            product.CPU_Id = cpu;
            product.Drive_Id = drive;
            product.Display_Id = display;
            product.Graphic_Id = graphic;
            product.RAM_Id = ram;
            product.Manufacturer_Id = manufacturer;
            product.Need_Id = need;
            product.Product_Status = status;
            product.Product_Description = detail;
            product.Product_Weight = weight;
            product.Product_Quantity = quantity;
            product.Product_Price = price;
            product.Product_Discount = discount;

            if (image!=null)
            {
                product.Image = image.FileName;
                string filePath = Path.Combine(Server.MapPath("~/Content/img/product"), image.FileName);
                image.SaveAs(filePath);
            }
            if(per==0)
                db.Product.Add(product);
            try
            {
                db.SaveChanges();
                return Json(new
                {
                    message = "ok"
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    message = "error"
                });
            }
        }
    }
}