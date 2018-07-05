using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Store.Models
{
    public class Product
    {
        
        public Product(bool addone = false)
        {
            if(addone == true)
            {
                ids++;
                IdProduct = ids;
            }
        }
        public Product(){ }

        public Product(Product p, bool addone = false)
        {
            IdProduct = p.IdProduct;
            Category = p.Category;
            Title = p.Title;
            ForKindAnimal = p.ForKindAnimal;
            Discription = p.Discription;
            Price = p.Price;
            
            if (addone == true)
            {
                ids++;
                IdProduct = ids;
            }
        }
        public void cloneDetail(Product p)
        {
            this.IdProduct = p.IdProduct;
            this.Category = p.Category;
            this.ForKindAnimal = p.ForKindAnimal;
            this.Discription = p.Discription;
            this.Price = p.Price;
            this.Title = p.Title;
        }

        public static int ids { get; set; }

        [Key]
        public int IdProduct { get; set; }

        [Required(ErrorMessage = "You must enter a category!")]
        [StringLength(15, MinimumLength = 2)]
        public String Category { get; set; }

        [Required(ErrorMessage = "You must enter a title!")]
        [StringLength(25, MinimumLength = 2)]
        public String Title { get; set; }

        
        [StringLength(15)]
        public String ForKindAnimal { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public String Discription { get; set; }

        [Required(ErrorMessage = "You must enter a price!")]
        [Range(0, double.MaxValue, ErrorMessage = "The value must be greater than 0")]
        public String Price { get; set; }

    }
}