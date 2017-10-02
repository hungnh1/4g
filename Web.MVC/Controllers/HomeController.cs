using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnF;
using Web.Repository;
namespace baohiem.Controllers
{
    public class HomeController : Controller
    {
        private Sim4GEntities db = new Sim4GEntities();
        public ActionResult Index()
        {

              ViewBag.GroupList = db.ProductGroups.Take(4).OrderBy(p=>p.Pos).ToList();

            return View();
        }

        public ActionResult Viewhtml()
        {
           
            return View();
        }

        
        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            CustommerOpinion custommerOpinions = new CustommerOpinion();
            custommerOpinions.Email = fc["yourmail"].ToString();
            db.CustommerOpinions.Add(custommerOpinions);
            db.SaveChanges();
            TempData["success"] = "true";
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View(db.Pages.First());
        }



        public ActionResult NewsList(int? paging)
        {
            if (paging == null)
            {
                paging = 1;
            }
            return View(db.Pages.OrderBy(p => p.PageId).Skip(paging.Value).Take(5).ToList().OrderByDescending(t => t.PageId).ToList());
        }

        public ActionResult NewsDetail(int? pageId)
        {            
          
         
            ViewData["ListProduct"] = db.Products.ToList();
           var pge = db.Pages.Where(p => p.PageId == pageId).ToList();

           if (pge.Count == 0|| pageId==null)
            {
                return View(db.Pages.ToList().First());
            }           

            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Contact(FormCollection fc)
        {
            var content = fc["fullname"] + " - " + fc["address"] + " - " + fc["email"]
                + " - " + fc["phone"] + ":" + fc["content"];
            MailReponsitory.Mail(fc["email"], content, "Góp ý");
            TempData["suucess"] = "true";
            try
            {
                MailReponsitory.Mail(fc["email"], fc["content"], "Góp ý");
                TempData["suucess"] = "true";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View("Contact");
        }




    }
}