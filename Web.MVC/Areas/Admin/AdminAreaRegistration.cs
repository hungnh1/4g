using System.Web.Mvc;

namespace baohiem.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            string[] namespaces = new[] { "baohiem.Areas.Admin.Controllers" };

            context.MapRoute(null, "connector", new { controller = "Files", action = "Index" });
            context.MapRoute(null, "Thumbnails/{tmb}", new { controller = "Files", action = "Thumbs", tmb = UrlParameter.Optional });

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Default1", action = "Index", id = UrlParameter.Optional },
                namespaces: namespaces
            );
           
        }
    }
}