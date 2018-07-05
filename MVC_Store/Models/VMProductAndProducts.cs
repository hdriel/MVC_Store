using MVC_Store.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Store.Models
{
    public class VMProductAndProducts
    {
        public User user { get; set; }
        public Product product { get; set; }
        public List<Product> products { get; set; }

        public VMProductAndProducts()
        {
            this.product = null;
            this.products = null;
        }

        public VMProductAndProducts(Product p, List<Product> ps)
        {
            this.product = p;
            this.products = ps;
        }

        public VMProductAndProducts(Product p)
        {
            ProductDal dal = new ProductDal();
            List<Product> products = (from x in dal.Products
                                      select x).ToList<Product>();
            this.product = p;
            this.products = null;
        }

        public VMProductAndProducts(List<Product> ps)
        {
            this.product = null;
            this.products = ps;
        }

        public VMProductAndProducts(VMProductAndProducts pps)
        {
            this.product = pps.product;
            this.products = pps.products;
        }


    }
}