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
            //  ViewBag.slide = db.ADVs.ToList();
            // ViewBag.tabProductlist = db.ProductGroups.Where(p => p.IsDefault == true).OrderBy(o => o.ParentId).Take(4).ToList();
            ViewBag.ProductGroup = db.ProductGroups.Where(p => p.Type == 1 & p.IsDefault != true).OrderByDescending(p => p.ProductGroupId).Skip(0).Take(4).ToList();
            ViewBag.Architecproductgroup = db.ProductGroups.Where(p => p.Type == 2 & p.IsDefault != true).OrderByDescending(p => p.ProductGroupId).Skip(0).Take(3).ToList();

            //   ViewBag.TopProduct = (from prt in db.Products
            //                         join pg in db.ProductGroups on prt.ProductGroupID
            //equals pg.ProductGroupId
            //                         where pg.IsDefault == true
            //                         select prt).OrderByDescending(o => o.ProductId).Take(4).ToList();
            ProductRepository productRepository = new ProductRepository();
            ViewData["TopProduct"] = productRepository.RetriveTopProduct();
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

        public ActionResult NewsDetail(int pageId)
        {

            return View(db.Pages.Where(p => p.PageId == pageId).First());
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