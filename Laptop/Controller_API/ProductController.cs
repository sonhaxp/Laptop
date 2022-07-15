using Laptop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Laptop.Controller_API
{
    public class ProductController : ApiController
    {
        LaptopDBContext1 db = new LaptopDBContext1();
        [HttpGet]
        public List<Product> GetList(int page,string key)
        {
            if (key == null||key=="")
            {
                return db.Product.OrderBy(x => x.Product_Id).Skip((page - 1) * 10).Take(10).ToList();
            }
            return db.Product.Where(x => x.Product_Name.Contains(key)).OrderBy(x=>x.Product_Id).Skip((page - 1) * 10).Take(10).ToList();
        }
        [HttpGet]
        public int CountOrder(string key)
        {
            if (key == null||key=="")
            {
                return (int)Math.Ceiling((double)db.Product.ToList().Count / 10);
            }
            return (int)Math.Ceiling((double)db.Product.Where(x => x.Product_Name.Contains(key)).ToList().Count / 10);
        }
        [HttpGet]
        public Product GetProduct(int id)
        {
            return db.Product.FirstOrDefault(x => x.Product_Id == id);
        }
    }
}
