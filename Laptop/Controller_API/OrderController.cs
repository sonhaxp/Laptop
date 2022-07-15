using Laptop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Laptop.Controller_API
{
    public class OrderController : ApiController
    {
        LaptopDBContext1 db = new LaptopDBContext1();
        [HttpGet]
        public List<Order> GetListOrder(int id, DateTime datefrom,DateTime dateto)
        {
            User user = (User)HttpContext.Current.Session["user"];
            return db.Order.Where(x => x.User_Id==user.User_Id && x.Order_Date >= datefrom && x.Order_Date <= dateto).OrderBy(x => x.Order_Id).ToList();
        }
        [HttpGet]
        public List<Order> GetListOrder(int status,int page)
        {
            return db.Order.Where(x => x.Order_Status==status).OrderBy(x=>x.Order_Id).Skip((page - 1) * 10).Take(10).ToList();
        }
        [HttpGet]
        public int CountOrder(int status)
        {
            return (int)Math.Ceiling((double)db.Order.Where(x=>x.Order_Status==status).ToList().Count / 10);
        }
    }
}
