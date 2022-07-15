using Laptop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laptop.Controllers
{
    public class RateController : Controller
    {
        // GET: Rate
        LaptopDBContext db = new LaptopDBContext();
        public ActionResult Rate(int id, int star)
        {
            User user = (User)Session["user"];
            if (user == null)
            {
                return Json(new
                {
                    message = "not-login"
                });
            }
            else
            {
                Rate_Detail rate = db.Rate_Detail.FirstOrDefault(x => x.User_Id == user.User_Id && x.Product_Id == id);
                if (rate == null)
                {
                    int v = db.Order.Where(x => x.User_Id == user.User_Id&&x.Order_Status==1).Join(db.Order_Detail.Where(x => x.Product_Id == id),
                                        entryPoint => entryPoint.Order_Id,
                                        entry => entry.Oder_Id,
                                        (entryPoint, entry) => new { entryPoint, entry }).ToList().Count;
                    if (v > 0)
                    {
                        rate = new Rate_Detail();
                        rate.Product_Id = id;
                        rate.User_Id = user.User_Id;
                        rate.Rate = star;
                        db.Rate_Detail.Add(rate);
                        db.SaveChanges();
                    }
                    else
                    {
                        return Json(new
                        {
                            message = "not-buy"
                        });
                    }
                }
                else
                {
                    rate.Rate = star;
                    db.SaveChanges();
                }
                Product product = db.Product.FirstOrDefault(x => x.Product_Id == id);
                return Json(new
                {
                    message = "ok",
                    star = product.Rate_Star,
                    quanlity = product.Quantity_Rate
                });
            }
        }
    }
}