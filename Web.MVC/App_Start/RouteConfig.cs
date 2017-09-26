using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace baohiem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
    name: "tintuc",
    url: "Tin-tuc/{name}-{pageId}",
    defaults: new
    {
        controller = "Home",
        action = "NewsDetail",
        pageId = UrlParameter.Optional
    }
);
            routes.MapRoute(
   name: "sanpham",
   url: "San-pham/{name}-{ProductId}",
   defaults: new
   {
       controller = "ProductList",
       action = "PaperProduct",
       ProductId = UrlParameter.Optional
   }
);
            routes.MapRoute(
                name: "dstin",
              url: "Tin/Tin-Bao-Minh",
                defaults: new { controller = "Home", action = "NewsList" }
            );

            routes.MapRoute(
                name: "nhomgiay",
              url: "Nhom-Giay-Dan/{name}-{ProductGroupId}",
                defaults: new { controller = "ProductList", action = "ProductList", ProductGroupId = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{pageId}",
                defaults: new { controller = "Home", action = "Index", pageId = UrlParameter.Optional }
            );

        }
    }
}
