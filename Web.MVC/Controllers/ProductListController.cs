using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnF;
using System.Globalization;
using Web.Repository;
using Newtonsoft.Json;
namespace baohiem.Controllers
{
    public class ProductListController : Controller
    {
        private Sim4GEntities db = new Sim4GEntities();
        //
        // GET: /Products/
        public ActionResult ProductList(int ProductGroupId)
        {

            var prolist = db.Products.Where(p => p.ProductGroupID == ProductGroupId).ToList();
            if (prolist.Count() == 0)
            {
                prolist = new List<Product>();
                ViewBag.groupname = db.ProductGroups.Find(ProductGroupId).Name;
            }
            else
            {
                ViewBag.groupname = prolist.First().ProductGroupName;
            }
            ViewBag.ProductGroupId = ProductGroupId;
            return View(prolist);
        }
        public ActionResult ProductDetail(int productId)
        {
            Product productdt = null;
            productdt = db.Products.Where(p => p.ProductId == productId).First();
         
            ViewBag.GroupList=db.Products.Where(p=>p.ProductGroupID== productdt.ProductGroupID).ToList();

            return View(productdt);
        }
        public ActionResult ArchiProctDetail(int productId)
        {
            Product productdt = null;
            productdt = db.Products.Where(p => p.ProductId == productId).First();
            ViewBag.Sameproduct = db.Products.Where(p => p.ProductGroupID == productdt.ProductGroupID & p.ProductId != productId).ToList();


            ImalistRoot response = new ImalistRoot();
            List<Imalist> listimg = new List<Imalist>();
            if (!string.IsNullOrEmpty(productdt.ImageList))
            {
                response = JsonConvert.DeserializeObject<ImalistRoot>(productdt.ImageList);
                listimg = response.imalist;

            }
            ViewBag.Imagelist = listimg;
            return View(productdt);
        }
        public ActionResult PaperProduct(int productId)
        {
            Product productdt = null;
            productdt = db.Products.Where(p => p.ProductId == productId).First();
            ViewBag.Grouplist = db.Products.Where(p => p.ProductGroupID == productdt.ProductGroupID).ToList();


            return View(productdt);
        }
        public ActionResult ArchitecProduct(int? ProductGroupId)
        {
            if (ProductGroupId == null)
            {
                List<ProductGroup> listGroup = db.ProductGroups.Where(p => p.Type == 2).ToList();

                if (listGroup.Count() > 0)
                {
                    ViewBag.groupname = listGroup.First().Name;
                    Int64 progroupId = listGroup.First().ProductGroupId;
                    return View(db.Products.Where(p => p.ProductGroupID == progroupId).ToList());
                }
                else
                {
                    ViewBag.groupname = "";
                    return View(new List<Product>());
                }
            }


            var prolist = db.Products.Where(p => p.ProductGroupID == ProductGroupId).ToList();
            if (prolist.Count == 0)
            {
                prolist = new List<Product>();
                ViewBag.groupname = db.ProductGroups.Find(ProductGroupId).Name;
            }
            else
            {
               // ViewBag.groupname = prolist.First().Name;
            }

            return View(prolist);


        }

        public ActionResult _PartialListGroup()
        {
            return PartialView(db.ProductGroups.Where(p => p.Type == 1 & p.IsDefault != true).ToList());
        }
        public ActionResult _PartialListArchiGroup()
        {
            return PartialView(db.ProductGroups.Where(p => p.Type == 2).ToList());
        }
        public ActionResult ProductGroup()
        {
            return View();
        }


    }
}