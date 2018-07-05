using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC_Store
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // ------------------------------------------------------------------------------------------------------
            routes.MapRoute(
               name: "Manager_Actions",
               url: "Actions",
               defaults: new { controller = "Manager", action = "Actions", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "Manager_Add",
               url: "AddNewProduct",
               defaults: new { controller = "Manager", action = "AddNewProduct", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "Manager_Update",
               url: "UpdateProduct",
               defaults: new { controller = "Manager", action = "UpdateProduct", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "Manager_Delete",
               url: "DeleteProduct",
               defaults: new { controller = "Manager", action = "DeleteProduct", id = UrlParameter.Optional }
           );
         // ------------------------------------------------------------------------------------------------------


            routes.MapRoute(
               name: "User_Edit",
               url: "Edit",
               defaults: new { controller = "User", action = "Edit", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "User_Profile",
               url: "Profile",
               defaults: new { controller = "User", action = "Profile", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "User_SignUp",
               url: "SignUp",
               defaults: new { controller = "User", action = "SignUp", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "User_LogIn",
              url: "LogIn",
              defaults: new { controller = "User", action = "LogIn", id = UrlParameter.Optional }
          );
            // ------------------------------------------------------------------------------------------------------

            routes.MapRoute(
               name: "Store_Home",
               url: "Store/Home",
               defaults: new { controller = "Store", action = "Home", id = UrlParameter.Optional }
           );

        // ------------------------------------------------------------------------------------------------------               
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
