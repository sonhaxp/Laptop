using Laptop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Laptop.Controller_API
{
    public class StatisticsController : ApiController
    {
        LaptopDBContext db = new LaptopDBContext();
        private long? quanlityOneDay()
        {
            return db.Order.Where(x => x.Order_Status == 1 
            && x.Order_Date.Value.Year == DateTime.Now.Year 
            && x.Order_Date.Value.Month == DateTime.Now.Month 
            && x.Order_Date.Value.Day == DateTime.Now.Day)
                .Join(db.Order_Detail,
                    entryPoint => entryPoint.Order_Id,
                    entry => entry.Oder_Id,
                    (entryPoint, entry) => new { entryPoint, entry }).Sum(x => x.entry.Quantity);
        }
        private long? quanlityOneMonth()
        {
            return db.Order.Where(x =>  x.Order_Status == 1 
            && x.Order_Date.Value.Year == DateTime.Now.Year
            && x.Order_Date.Value.Month == DateTime.Now.Month)
                .Join(db.Order_Detail,
                    entryPoint => entryPoint.Order_Id,
                    entry => entry.Oder_Id,
                    (entryPoint, entry) => new { entryPoint, entry }).Sum(x => x.entry.Quantity);
        }
        private long? RevenueOneDay()
        {
            return db.Order.Where(x =>  x.Order_Status == 1 
            && x.Order_Date.Value.Year == DateTime.Now.Year 
            && x.Order_Date.Value.Month == DateTime.Now.Month 
            && x.Order_Date.Value.Day == DateTime.Now.Day)
                .Sum(x => x.Total);
        }
        private long? RevenueOneMonth()
        {
            return db.Order.Where(x =>x.Order_Status==1
            && x.Order_Date.Value.Year == DateTime.Now.Year 
            && x.Order_Date.Value.Month == DateTime.Now.Month)
                .Sum(x => x.Total);
        }
        [HttpGet]
        public List<long?> Statitics()
        {
            List<long?> list = new List<long?>();
            long? quanlity1Day = quanlityOneDay();
            long? quanlity1Month = quanlityOneMonth();
            long? Revenue1Day = RevenueOneDay();
            long? Revenue1Month = RevenueOneMonth();
            list.Add(quanlity1Day == null?0: quanlity1Day);
            list.Add(quanlity1Month == null ? 0 : quanlity1Month);
            list.Add(Revenue1Day == null ? 0 : Revenue1Day);
            list.Add(Revenue1Month == null ? 0 : Revenue1Month);
            return list;
        }
        [HttpGet]
        [Route("getReveneManufacturer")]
        public List<List<string>> GetReveneManufacturer()
        {
            List<List<string>> Revene = new List<List<string>>();
            List<Manufacturer> manufacturer = db.Manufacturer.ToList();
            foreach(Manufacturer i in manufacturer)
            {
                List<string> list = new List<string>();
                list.Add(i.Manufacturer_Name);
                long? total = db.Order.Where(x => x.Order_Status == 1)
                .Join(db.Order_Detail,
                    entryPoint => entryPoint.Order_Id,
                    entry => entry.Oder_Id,
                    (entryPoint, entry) => new { entryPoint, entry })
                .Join(
                    db.Product,
                    combinedEntry => combinedEntry.entry.Product_Id,
                    title => title.Product_Id,
                    (combinedEntry, title) => new
                    {
                        combinedEntry,
                        title
                    }
                ).Where(x => x.title.Manufacturer_Id == i.Manufacturer_Id)
                .Sum(x => x.combinedEntry.entry.Total);
                list.Add(total == null ? "0" : total.Value.ToString());
                Revene.Add(list);
            }   
            return Revene;
        }
        [HttpGet]
        [Route("getRevene")]
        public List<List<string>> GetRevene()
        {
            List<List<string>> Revene = new List<List<string>>();
            DateTime now = DateTime.Now;
            List<DateTime> month = new List<DateTime>();
            for (int i = 11; i >= 0; i--)
            {
                month.Add(DateTime.Now.AddMonths(-i));
            }
            for (int i = 0 ;i < month.Count;i++)
            {
                List<string> list = new List<string>();
                list.Add(month[i].ToShortDateString().Split('/')[1] + "/" + month[i].ToShortDateString().Split('/')[2]);
                int m = month[i].Month;
                int y = month[i].Year;
                long? total = db.Order.Where(x => x.Order_Status == 1&&x.Order_Date.Value.Month==m
                                            && x.Order_Date.Value.Year==y)
                .Sum(x => x.Total);
                list.Add(total == null ? "0" : total.Value.ToString());
                Revene.Add(list);
            }
            return Revene;
        }
        [HttpGet]
        [Route("getTopUser")]
        public List<List<string>> getTopUser()
        {
            List<List<string>> users = new List<List<string>>();
            var user = db.Order.Where(x => x.Order_Status == 1 && x.Order_Date.Value.Year == DateTime.Now.Year)
                .GroupBy(x => x.User_Id)
                .Select(x => new { UserId = x.Key, Total = x.Select(g => g.Total).Sum()})
                .OrderByDescending(x=>x.Total).Take(5).Join(db.User.Where(x=>x.Role_Id==2),
                    entryPoint => entryPoint.UserId,
                    entry => entry.User_Id,
                    (entryPoint, entry) => new { entryPoint, entry });
            foreach(var i in user)
            {
                List<string> list = new List<string>();
                list.Add(i.entry.Name);
                list.Add(i.entry.Email);
                list.Add(i.entry.Address);
                list.Add(i.entry.PhoneNumber);
                list.Add(i.entryPoint.Total.ToString());
                users.Add(list);
            }
            return users;
        }
    }
}
