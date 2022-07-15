using Laptop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laptop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Render_NavBar()
        {
            LaptopDBContext db = new LaptopDBContext();
            ViewBag.Manufacturer = db.Manufacturer.Where(x=>x.Status_Id==true).ToList();
            ViewBag.Need = db.Need.Where(x => x.Need_Status == true).ToList();
            return PartialView("_Product_NavBar");
        }
        public ActionResult Render_Product()
        {
            LaptopDBContext db = new LaptopDBContext();
            ViewBag.Product = db.Product.OrderByDescending(x=>x.Quantity_Sold).Take(12).ToList();
            return PartialView("_Product_Main", (int)Math.Ceiling((double)db.Product.ToList().Count / 12));
        }
        private int[] convertStringtoArrInt(string str)
        {
            string[] list = str.Split(',');
            int[] arr = new int[list.Length];
            for (int i=0;i<list.Length;i++)
            {
                arr[i] = int.Parse(list[i]);
            }
            return arr;
        }
        [HttpPost]
        public ActionResult getProduct(int page, string manufacturer, string need, string price, int orderby)
        {
            LaptopDBContext db = new LaptopDBContext();
            int[] list_manufacturer;
            List<Product> products = new List<Product>();
            if (manufacturer != "")
            {
                list_manufacturer = convertStringtoArrInt(manufacturer);
                foreach (var i in db.Product.ToList())
                {
                    foreach (int j in list_manufacturer)
                    {
                        if (i.Manufacturer_Id == j)
                        {
                            products.Add(i);
                            break;
                        }
                    }
                }
            }
            int[] list_need;
            List<Product> products1 = new List<Product>();
            if (need != "")
            {
                list_need = convertStringtoArrInt(need);
                foreach (var i in db.Product.ToList())
                {
                    foreach (int j in list_need)
                    {
                        if (i.Need_Id == j)
                        {
                            products1.Add(i);
                            break;
                        }
                    }
                }
            }
            int[] list_price;
            List<Product> products2 = new List<Product>();
            if (price != "")
            {
                list_price = convertStringtoArrInt(price);
                foreach (var i in db.Product.ToList())
                {
                    foreach (int j in list_price)
                    {
                        if (j == 1)
                        {
                            if (i.Product_Price * (1 - i.Product_Discount / 100) <= 10000000)
                            {
                                products2.Add(i);
                                break;
                            }
                        }
                        else if (j == 2)
                        {
                            if (i.Product_Price * (1 - i.Product_Discount / 100) <= 15000000 && i.Product_Price * (1 - i.Product_Discount / 100) > 10000000)
                            {
                                products2.Add(i);
                                break;
                            }
                        }
                        else if (j == 3)
                        {
                            if (i.Product_Price * (1 - i.Product_Discount / 100) <= 20000000 && i.Product_Price * (1 - i.Product_Discount / 100) > 15000000)
                            {
                                products2.Add(i);
                                break;
                            }
                        }
                        else if (j == 4)
                        {
                            if (i.Product_Price * (1 - i.Product_Discount / 100) <= 25000000 && i.Product_Price * (1 - i.Product_Discount / 100) > 20000000)
                            {
                                products2.Add(i);
                                break;
                            }
                        }
                        else if (j == 5)
                        {
                            if (i.Product_Price * (1 - i.Product_Discount / 100) > 25000000)
                            {
                                products2.Add(i);
                                break;
                            }
                        }
                    }
                }
            }
            List<Product> res = db.Product.ToList();
            if (manufacturer != "")
            {
                res = res.Intersect(products).ToList();
            }
            if (need != "")
            {
                res = res.Intersect(products1).ToList();
            }
            if (price != "")
            {
                res = res.Intersect(products2).ToList();
            }
            int count = (int)Math.Ceiling((double)res.Count / 12);
            if (orderby == 0)
            {
                res = res.OrderByDescending(x => x.Quantity_Sold).ToList();
            }
            else if (orderby == 1)
            {
                res = res.OrderBy(x => x.Product_Price * (1 - x.Product_Discount / 100)).ToList();
            }
            else if (orderby == 2)
            {
                res = res.OrderByDescending(x => x.Product_Price * (1 - x.Product_Discount / 100)).ToList();
            }
            else if (orderby == 3)
            {
                res = res.OrderByDescending(x => x.Rate_Star).ToList();
            }
            else if (orderby == 4)
            {
                res = res.OrderByDescending(x => x.Quantity_Wish).ToList();
            }
            ViewBag.Product = res.Skip((page - 1) * 12).Take(12).ToList();
            return PartialView("_Product_Main", count);
            //return (int)Math.Ceiling((double)db.Product.Where(x => x.Product_Name.Contains(key)).ToList().Count / 10);
        }
        //[HttpPost]
        //public ActionResult getCountProduct(string manufacturer, string need, string price)
        //{
        //    LaptopDBContext db = new LaptopDBContext();
        //    int[] list_manufacturer;
        //    List<Product> products = new List<Product>();
        //    if (manufacturer != "")
        //    {
        //        list_manufacturer = convertStringtoArrInt(manufacturer);
        //        foreach (var i in db.Product.ToList())
        //        {
        //            foreach (int j in list_manufacturer)
        //            {
        //                if (i.Manufacturer_Id == j)
        //                {
        //                    products.Add(i);
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    int[] list_need ;
        //    List<Product> products1 = new List<Product>();
        //    if (need != "")
        //    {
        //        list_need = convertStringtoArrInt(need);
        //        foreach (var i in db.Product.ToList())
        //        {
        //            foreach (int j in list_need)
        //            {
        //                if (i.Need_Id == j)
        //                {
        //                    products1.Add(i);
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    int[] list_price;
        //    List<Product> products2 = new List<Product>();
        //    if (price != "")
        //    {
        //        list_price = convertStringtoArrInt(price);
        //        foreach (var i in db.Product.ToList())
        //        {
        //            foreach (int j in list_price)
        //            {
        //                if (j == 1)
        //                {
        //                    if (i.Product_Price * (1 - i.Product_Discount / 100) <= 10000000)
        //                    {
        //                        products2.Add(i);
        //                        break;
        //                    }
        //                }
        //                else if (j == 2)
        //                {
        //                    if (i.Product_Price * (1 - i.Product_Discount / 100) <= 15000000&& i.Product_Price * (1 - i.Product_Discount / 100) > 10000000)
        //                    {
        //                        products2.Add(i);
        //                        break;
        //                    }
        //                }
        //                else if (j == 3)
        //                {
        //                    if (i.Product_Price * (1 - i.Product_Discount / 100) <= 20000000 && i.Product_Price * (1 - i.Product_Discount / 100) > 15000000)
        //                    {
        //                        products2.Add(i);
        //                        break;
        //                    }
        //                }
        //                else if (j == 4)
        //                {
        //                    if (i.Product_Price * (1 - i.Product_Discount / 100) <= 25000000 && i.Product_Price * (1 - i.Product_Discount / 100) > 20000000)
        //                    {
        //                        products2.Add(i);
        //                        break;
        //                    }
        //                }
        //                else if (j == 5)
        //                {
        //                    if (i.Product_Price * (1 - i.Product_Discount / 100) > 25000000)
        //                    {
        //                        products2.Add(i);
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    List<Product> res = db.Product.ToList();
        //    if (manufacturer != "")
        //    {
        //        res = res.Intersect(products).ToList();
        //    }
        //    if (need != "")
        //    {
        //        res = res.Intersect(products1).ToList();
        //    }
        //    if (price != "")
        //    {
        //        res = res.Intersect(products2).ToList();
        //    }
        //    int count = (int)Math.Ceiling((double)res.Count / 12);
        //    //return (int)Math.Ceiling((double)db.Product.Where(x => x.Product_Name.Contains(key)).ToList().Count / 10);
        //    return Json(new
        //    {
        //        count = count
        //    });
        //}
    }
}