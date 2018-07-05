using MVC_Store.Dal;
using MVC_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MVC_Store.Controllers
{
    public class StoreController : Controller
    {
        static User main_user = null;

        public ActionResult About()
        {
            return View("About", main_user);
        }

        [HttpPost]
        public ActionResult BuyProduct(Product p)
        {
            ProductOfUserDal dal = new ProductOfUserDal();
            ProductsOfUser pou = new ProductsOfUser(main_user, p);
            dal.ProductsOfUser.Add(pou);
            dal.SaveChanges();
            Thread.Sleep(2000);
            return Json(new ProductDal().Products.ToList<Product>(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Home(User user)
        {
            User loginguser = (User) TempData["userLogIn"];
            if(loginguser != null)
                main_user = loginguser;
            else if(user != null)
                main_user = user;

            ProductDal dal = new ProductDal();
            List<Product> products = dal.Products.ToList<Product>();

            
            if (main_user == null)
            {
                return View("Home", new VMUserAndProducrs(products));
            }
            else
            {
                return View("Home", new VMUserAndProducrs(main_user, products));
            }
        }

        [HttpGet]
        public ActionResult GetProductsByJson()
        {
            ProductDal dal = new ProductDal();
            List<Product> products = (from x in dal.Products
                                      select x).ToList<Product>();
            Thread.Sleep(2000);
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        
        [HttpPost]
        public ActionResult SearchByStringText(Product p)
        {
            String text = p.Title;
            ProductDal dal = new ProductDal();

            Thread.Sleep(2000);

            if (text != null && text != "")
            {
                List<Product> products = (from x in dal.Products
                                          where x.Category.Contains(text) || x.Discription.Contains(text) || x.ForKindAnimal.Contains(text)
                                          select x).ToList<Product>();

                return Json(products, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Product> products = (from x in dal.Products
                                          select x).ToList<Product>();
                return Json(products, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult myOrdersAction()
        {
            HistoricalProductDal dalPro = new HistoricalProductDal();
            ProductOfUserDal dalProOfUser = new ProductOfUserDal();

            List<int> productsKeys = (from x in dalProOfUser.ProductsOfUser 
                                      select x.productKey).ToList<int>();

            List<Product> products = new List<Product>();

            for (int i = 0; i < productsKeys.Count(); i++)
            {
                int key = productsKeys.ElementAt(i);
                Product proofuser = dalPro.Products.FirstOrDefault(x => x.IdProduct.Equals(key));
                products.Add(proofuser);
            }
           
            return View("myOrdersAction", new VMUserAndProducrs(main_user, products));
        }

        [HttpPost]
        public ActionResult DeleteProductFromDBByJson(Product p)
        {
            ProductDal dal = new ProductDal();
            Product productToRemove = dal.Products.FirstOrDefault(x => x.IdProduct.Equals(p.IdProduct));
            dal.Products.Remove(productToRemove);
            dal.SaveChanges();
            List<Product> products = dal.Products.ToList<Product>();
            Thread.Sleep(2000);
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteProductFromUserByJson(Product p)
        {
            ProductOfUserDal dal = new ProductOfUserDal();
            ProductsOfUser productToRemove = dal.ProductsOfUser.FirstOrDefault(x => x.productKey.Equals(p.IdProduct) && x.userKey.Equals(main_user.Email));
            dal.ProductsOfUser.Remove(productToRemove);
            dal.SaveChanges();

            ProductDal dalPro = new ProductDal();
            List<int> productsKeys = (from x in dal.ProductsOfUser
                                      select x.productKey).ToList<int>();

            List<Product> products = (from x in dalPro.Products
                                      where productsKeys.Contains(x.IdProduct)
                                      select x).ToList<Product>();
            Thread.Sleep(2000);
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetMyProductsOrdersByJson()
        {
            HistoricalProductDal dalPro = new HistoricalProductDal();
            ProductOfUserDal dalProOfUser = new ProductOfUserDal();

            List<int> productsKeys = (from x in dalProOfUser.ProductsOfUser
                                      select x.productKey).ToList<int>();

            List<Product> products = new List<Product>();

            for (int i = 0; i < productsKeys.Count(); i++)
            {
                int key = productsKeys.ElementAt(i);
                Product proofuser = dalPro.Products.FirstOrDefault(x => x.IdProduct.Equals(key));
                products.Add(proofuser);
            }
            Thread.Sleep(2000);
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        // GET: Store
        public ActionResult Index()
        {
            return View();
        }
    }
}