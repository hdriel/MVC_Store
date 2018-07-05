using MVC_Store.Dal;
using MVC_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Serialization;
using System.Data.Entity;

namespace MVC_Store.Controllers
{
    public class ManagerController : Controller
    {
        static User manager;

        private VMProductAndProducts searching()
        {
            string strIdProduct = Request.Form["IdProduct"];
            string strcategory = Request.Form["Category"];
            string strAnimal = Request.Form["Animal"];
            ProductDal dal = new ProductDal();
            VMProductAndProducts pps = new VMProductAndProducts();
            List<Product> products = new List<Product>();
            if (strcategory != null && strcategory != "")
            {
                products = (from x in dal.Products
                            where x.Category.Contains(strcategory)
                            select x).ToList<Product>();
            }
            else if (strAnimal != null && strAnimal != "")
            {
                products = (from x in dal.Products
                            where x.ForKindAnimal.Contains(strAnimal)
                            select x).ToList<Product>();
                pps.products = products;
            }
            else if (strIdProduct != null && strIdProduct != "")
            {
                int id = Int32.Parse(strIdProduct);
                products = (from x in dal.Products
                            where x.IdProduct.Equals(id)
                            select x).ToList<Product>();
                pps.products = products;
            }

            if (products.Count() > 0 && products.Count() == 1)
            {
                pps.product = products.ElementAt(0);
                pps.products = products;
            }
            else if (products.Count() > 0)
            {
                pps.product = null;
                pps.products = products;
            }
            else
            {
                pps.product = null;
                pps.products = null;
            }
            pps.user = manager;
            return pps;
        }

        public ActionResult UpdateProduct_byActionLink(Product pro)
        {
            VMProductAndProducts pps = new VMProductAndProducts(pro);
            pps.products = new List<Product>();
            pps.products.Add(pro);
            pps.user = manager;
            return View("UpdateProduct", pps);
        }

        public ActionResult UpdateProduct(VMProductAndProducts pps)
        {
            pps.user = manager;
            return View("UpdateProduct", pps);
        }
        public ActionResult SearchUpdate()
        {
            return View("UpdateProduct", searching());
        }
        public ActionResult SubmitUpdate(VMProductAndProducts pps)
        {
            if (ModelState.IsValid)
            {
                Product p = pps.product;
                List<Product> ps = pps.products;
                Product fromDB;
                ProductDal dal = new ProductDal();
                //fromDB = dal.Products.FirstOrDefault(x => x.IdProduct.Equals(pps.product.IdProduct));
                List<Product> products = (from x in dal.Products
                                          where x.IdProduct.Equals(pps.product.IdProduct)
                                          select x).ToList<Product>();


                if (products != null && products.Count() == 1)
                {
                    fromDB = products.ElementAt(0);
                    fromDB.cloneDetail(pps.product);
                }
                try
                {
                    dal.SaveChanges();
                    HistoricalProductDal dalHistori = new HistoricalProductDal();
                    Product fromDBHisrori = dalHistori.Products.FirstOrDefault(x => x.IdProduct.Equals(pps.product.IdProduct));
                    dalHistori.Products.Remove(fromDBHisrori);
                    dalHistori.Products.Add(pps.product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return View("UpdateProduct", pps);
                }
                return View("Actions", new VMUserAndProducrs(manager));

            }
            return View("UpdateProduct", pps);
        }

        // ------------------------------------------------------------------------

        public ActionResult AddNewProduct()
        {         
            return View("AddNewProduct", new VM_UserAndProduct_help(manager));
        }
        public ActionResult SubmitAdd(VM_UserAndProduct_help up)
        {
            Product p = up.product;
            if (ModelState.IsValid)
            {

                Product product = new Product(p, true);

                ProductDal dal = new ProductDal();
                dal.Products.Add(p);
                dal.SaveChanges();

                HistoricalProductDal dalHistori = new HistoricalProductDal();
                dalHistori.Products.Add(p);
                dalHistori.SaveChanges();

                return View("Actions", new VMUserAndProducrs(manager));
            }
            else
                return View("AddNewProduct", p);
        }

        // ------------------------------------------------------------------------

        public ActionResult DeleteProduct(VMProductAndProducts pps)
        {
            pps.user = manager;
            return View("DeleteProduct", pps);
        }
        public ActionResult SubmitDelete(VMProductAndProducts pps)
        {
            if (ModelState.IsValid)
            {
                ProductDal dal = new ProductDal();
                Product productRemove = dal.Products.FirstOrDefault(x => x.IdProduct.Equals(pps.product.IdProduct));
                dal.Products.Remove(productRemove);
                dal.SaveChanges();
                return View("Actions", new VMUserAndProducrs(manager));
            }
            else
                return View("DeleteProduct");
        }
        public ActionResult searchDelete()
        {
            return View("DeleteProduct" , searching());
        }

        public ActionResult DeleteProduct_byActionLink(Product pro)
        {
            VMProductAndProducts pps = new VMProductAndProducts(pro);
            pps.products = new List<Product>();
            pps.products.Add(pro);

            ProductDal dal = new ProductDal();
            Product productRemove = dal.Products.FirstOrDefault(x => x.IdProduct.Equals(pps.product.IdProduct));
            dal.Products.Remove(productRemove);
            dal.SaveChanges();
            return View("Actions", new VMUserAndProducrs(manager));
        }

        // ------------------------------------------------------------------------

        public ActionResult ShowUsers(VMUserAndUsers us)
        {
            UserDal dal = new UserDal();
            User userToUpdateAge;
            IQueryable<User> rtn = from temp in dal.Users select temp;
            //List<User> listusers = (from temp in dal.Users select temp).ToList<User>();

            List<User> users = (from x in dal.Users
                                select x).ToList<User>();

            for (int i = 0; i < users.Count(); i++)
            {
                userToUpdateAge = users.ElementAt(i);
                userToUpdateAge.UpdateAge();
                int a = userToUpdateAge.age;
                dal.Entry(userToUpdateAge).State = EntityState.Modified;
            }
            dal.SaveChanges();

            us.user = manager; 
            return View("ShowUsers", us);
        }
        public ActionResult DeleteUser_byActionLink(User user)
        {
            UserDal dal = new UserDal();
            User userToRemove = dal.Users.FirstOrDefault(x => x.Email.Equals(user.Email));
            dal.Users.Remove(userToRemove);
            dal.SaveChanges();
            return View("Actions", new VMUserAndProducrs(manager));
        }
        public ActionResult UpdateUser_byActionLink(User user)
        {
            VMUserAndUsers uus = new VMUserAndUsers(user);
            uus.users = new List<User>();
            return View("ShowUsers", uus);
        }

        public ActionResult UpdateUser_byJson()
        {
            UserDal dal = new UserDal();
            User Juser = new User();
            Juser.Email = Request.Form["Email"];
            Juser = dal.Users.FirstOrDefault(x => x.Email.Equals(Juser.Email));
            Juser.FirstName = Request.Form["FirstName"];
            VMUserAndUsers uus = new VMUserAndUsers(Juser);
            uus.users = new List<User>();
            return View("ShowUsers", uus);
        }


        // ------------------------------------------------------------------------
        public ActionResult Actions(User vm)
        {
            ProductDal dal = new ProductDal();
            List<Product> products = (from x in dal.Products
                                      select x).ToList<Product>();
            
            manager = vm;

            if (vm != null)
            {
                return View("Actions", new VMUserAndProducrs(vm, products));
            }
            return RedirectToAction("Home", "Store", vm);
        }


        // ------------------------------------------------------------------------



        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }
    }

    [Serializable]
    internal class DbUpdateConcurrencyException : Exception
    {
        public DbUpdateConcurrencyException()
        {
        }

        public DbUpdateConcurrencyException(string message) : base(message)
        {
        }

        public DbUpdateConcurrencyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DbUpdateConcurrencyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}