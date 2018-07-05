using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Store.Models
{
    public class VM_UserAndProduct_help
    {
        public User user { get; set; }
        public Product product { get; set; }
        
        public VM_UserAndProduct_help()
        {
            this.user = null;
            this.product = null;
        }
        public VM_UserAndProduct_help(User user)
        {
            this.user = user;
        }
        public VM_UserAndProduct_help(Product product, User user)
        {
            this.user = user;
            this.product = product;
        }

        public VM_UserAndProduct_help(User user, Product product)
        {
            this.user = user;
            this.product = product;
        }

    }
}