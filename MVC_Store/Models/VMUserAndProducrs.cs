using MVC_Store.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Store.Models
{
    public class VMUserAndProducrs
    {
        public User user { get; set; }
        public List<Product> products { get; set; }

        public VMUserAndProducrs()
        {
            ProductDal dal = new ProductDal();
            List<Product> products = (from x in dal.Products
                                      select x).ToList<Product>();
            this.user = new User();
            this.products = products;
        }

        public VMUserAndProducrs(User u)
        {
            ProductDal dal = new ProductDal();
            List<Product> products = (from x in dal.Products
                                      select x).ToList<Product>();
            this.user = u;
            this.products = products;
        }

        public VMUserAndProducrs(List<Product> p)
        {
            this.user = new User();
            this.products = p;
        }

        public VMUserAndProducrs(List<Product> p, User u)
        {
            this.user = u;
            this.products = p;
        }

        public VMUserAndProducrs(User u, List<Product> p)
        {
            this.user = u;
            this.products = p;
        }
    }
}