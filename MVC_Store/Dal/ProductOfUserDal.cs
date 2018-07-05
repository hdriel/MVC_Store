using MVC_Store.Controllers;
using MVC_Store.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC_Store.Dal
{
    public class ProductOfUserDal : DbContext
    {

      
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductsOfUser>().ToTable("tblProductsOfUser");
        }

        public DbSet<ProductsOfUser> ProductsOfUser { get; set; }
    }
}