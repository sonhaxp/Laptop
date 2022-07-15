using Laptop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Laptop.Controller_API
{
    public class UserController : ApiController
    {
        LaptopDBContext1 db = new LaptopDBContext1();
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
        [HttpGet]
        public User Login(string username)
        {
            User a = db.User.Find(username);
            return a;
        }
        [HttpGet]
        public User Login(string username, string password)
        {
            User a = db.User.Find(username);
            if (a == null) return null;
            if (a.Password == GetMD5(password))
            {
                return a;
            }
            return null;
        }
        [HttpPost]
        public bool AddUser(string name, string email,string password, DateTime birthday,string gender, int role, string phone,string address)
        {
            if (db.User.Where(x => x.Email == email)!=null) return false;
            User user = new User();
            user.Name = name;
            user.Birthday = birthday;
            user.Gender = gender;
            user.PhoneNumber = phone;
            user.Address = address;
            user.Email = email;
            user.Password = GetMD5(password);
            user.Role_Id = role;
            try
            {
                db.User.Add(user);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        [HttpPut]
        public bool UpdateUser(string email, string password)
        {
            User user = db.User.FirstOrDefault(x => x.Email == email);
            user.Password = GetMD5(password);
            db.SaveChanges();
            return true;
        }
        [HttpDelete]
        public bool DeleteUser(string email)
        {
            User user = db.User.FirstOrDefault(x => x.Email == email);
            try
            {
                db.User.Remove(user);
                db.SaveChanges();
            }
            catch (Exception)
            {
                user.UserStatus = false;
                db.SaveChanges();
                return false;
            }
            return true;

        }
    }
}
