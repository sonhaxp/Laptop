using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Laptop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "dang-ki",
                url: "dang-ki",
                defaults: new { controller = "User", action = "Register" }
            );
            routes.MapRoute(
                name: "thong-tin",
                url: "thong-tin",
                defaults: new { controller = "User", action = "Info" }
            );
            routes.MapRoute(
                name: "dang-xuat",
                url: "dang-xuat",
                defaults: new { controller = "User", action = "Logout" }
            );
            routes.MapRoute(
                name: "thanh-toan",
                url: "thanh-toan",
                defaults: new { controller = "Cart", action = "checkout" }
            );
            routes.MapRoute(
                name: "gio-hang",
                url: "gio-hang",
                defaults: new { controller = "Cart", action = "Index" }
            );
            routes.MapRoute(
                name: "yeu-thich",
                url: "yeu-thich",
                defaults: new { controller = "WishList", action = "Index" }
            );
            routes.MapRoute(
                name: "dang-nhap",
                url: "dang-nhap",
                defaults: new { controller = "User", action = "Login" }
            );
            routes.MapRoute(
                name: "trang-chu",
                url: "trang-chu",
                defaults: new { controller = "Home", action = "Index" }
            );
            routes.MapRoute(
                name: "san-pham",
                url: "san-pham",
                defaults: new { controller = "Product", action = "Index" }
            );
            routes.MapRoute(
                name: "tim-kiem",
                url: "tim-kiem/{id}",
                defaults: new { controller = "Search", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "thong-tin-san-pham",
                url: "san-pham/{id}",
                defaults: new { controller = "ProductDetail", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
