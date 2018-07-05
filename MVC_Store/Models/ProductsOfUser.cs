using MVC_Store.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Store.Controllers
{
    public class ProductsOfUser
    {
       
        [Key]
        [Column(Order = 1)]
        public String userKey { get; set; }

        [Key]
        [Column(Order = 2)]
        public int productKey { get; set; }


        public DateTime dateOfPurchase { get; set; }

        public ProductsOfUser()
        {
            this.userKey = null;
            this.productKey = 0;
            this.dateOfPurchase = DateTime.Today;
        }
        public ProductsOfUser(Product product, User user)
        {
            this.userKey = user.Email;
            this.productKey = product.IdProduct;
            this.dateOfPurchase = DateTime.Today;
        }

        public ProductsOfUser(User user, Product product)
        {
            this.userKey = user.Email;
            this.productKey = product.IdProduct;
            this.dateOfPurchase = DateTime.Today;
        }

    }
}