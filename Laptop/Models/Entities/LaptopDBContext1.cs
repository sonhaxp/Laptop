using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Laptop.Models.Entities
{
    public partial class LaptopDBContext1 : DbContext
    {
        public LaptopDBContext1()
            : base("name=LaptopDBContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Cart_Detail> Cart_Detail { get; set; }
        public virtual DbSet<CPU> CPU { get; set; }
        public virtual DbSet<Display> Display { get; set; }
        public virtual DbSet<Drive> Drive { get; set; }
        public virtual DbSet<Graphic> Graphic { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<Need> Need { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Order_Detail> Order_Detail { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Provider> Provider { get; set; }
        public virtual DbSet<Purchase> Purchase { get; set; }
        public virtual DbSet<Purchase_Detail> Purchase_Detail { get; set; }
        public virtual DbSet<RAM> RAM { get; set; }
        public virtual DbSet<Rate_Detail> Rate_Detail { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<WishList> WishList { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Order_Detail)
                .WithRequired(e => e.Order)
                .HasForeignKey(e => e.Oder_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Cart_Detail)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Order_Detail)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Purchase_Detail)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Rate_Detail)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.WishList)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Provider>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Purchase>()
                .HasMany(e => e.Purchase_Detail)
                .WithRequired(e => e.Purchase)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Cart_Detail)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Order)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.Seller_Id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Order1)
                .WithOptional(e => e.User1)
                .HasForeignKey(e => e.Seller_Id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Rate_Detail)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.WishList)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
