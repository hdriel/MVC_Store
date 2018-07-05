using MVC_Store.Dal;
using MVC_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Store.Controllers
{
    public class UserController : Controller
    {
        static User main_user = null;

        public ActionResult UpdateDataUser(User user)
        {
            if (ModelState.IsValid)
            {
                main_user = user;
                return RedirectToAction("Profile", user);
            }
            else
                return View("Profile", user);
        }

        public ActionResult SubmitSignUp(User user)
        {
            if(ModelState.IsValid)
            {
                var today = DateTime.Today;
                var a = (today.Year * 100 + today.Month) * 100 + today.Day;
                var b = (user.BirthDate.Year * 100 + user.BirthDate.Month) * 100 + user.BirthDate.Day;
                user.age =  (a - b) / 10000;

                UserDal dal = new UserDal();
                dal.Users.Add(user);
                dal.SaveChanges();
                return RedirectToAction("LogIn" , user);
            }
            else
                return View("SignUp", user);
        }

        public ActionResult SubmitLogIn(User u)
        {
            // check if this user found in data base
            User checkingUser = null;
            UserDal dal = new UserDal();
            List<User> users = (from x in dal.Users
                         where x.Email == u.Email
                         select x).ToList<User>();

            if(users.Count() == 1)
            {
                checkingUser = users.ElementAt(0);
            }
            if (checkingUser != null)
            {
                main_user = checkingUser;
                TempData["userLogIn"] = checkingUser;
                return RedirectToAction("Home", "Store");
            }
            else
                return View("LogIn", u);
        }

        public ActionResult Edit(User user)
        {
            return View("Edit", user);
        }

        public ActionResult Profile(User user)
        {
            return View("Profile", main_user);
        }

        public ActionResult SignUp(User user)
        {
            return View("SignUp", user);
        }

        public ActionResult LogIn(User user)
        {
            return View("LogIn", user);
        }

        public ActionResult LogOut()
        {
            main_user = null;
            return RedirectToAction("Home", "Store", main_user);
        }


        // GET: User
        public ActionResult Index()
        {
            return View();
        }
    }
}