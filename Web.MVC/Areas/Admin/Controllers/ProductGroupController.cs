using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EnF;
using System.IO;
using System.Data.SqlClient;
using Web.Repository;
namespace baohiem.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductGroupController : Controller
    {
        private DayGianTuongEntities db = new DayGianTuongEntities();

        // GET: /Admin/ProductGroup/
        public ActionResult Index()
        {
          
           
            return View(db.ProductGroups.Where(p=>p.Type==1& p.IsDefault!=true).ToList());
        }

        // GET: /Admin/ProductGroup/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroup productgroup = db.ProductGroups.Find(id);
            if (productgroup == null)
            {
                return HttpNotFound();
            }
            return View(productgroup);
        }

        // GET: /Admin/ProductGroup/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: /Admin/ProductGroup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductGroup productgroup)
        {
            if (ModelState.IsValid)
            {
                productgroup.Type =1;
                db.ProductGroups.Add(productgroup);
                db.SaveChanges();
                if (Request.Files[0].ContentLength != 0)
                {
                    if (Request.Files[0].FileName.Contains(".jpg") || Request.Files[0].FileName.Contains(".png")
                    || Request.Files[0].FileName.Contains(".gif"))
                    {
                        string pathToSaveimage = Server.MapPath("/Storedata/Productlist/" + productgroup.ProductGroupId);//Phần vị trí lưu File .
                        CreateFolder(pathToSaveimage);
                        string filename = Path.GetFileName(Request.Files[0].FileName);
                        Request.Files[0].SaveAs(Path.Combine(pathToSaveimage, filename));

                        productgroup.Image = "Storedata/Productlist/" + productgroup.ProductGroupId + "/" + filename;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }

            return View(productgroup);
        }

        // GET: /Admin/ProductGroup/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroup productgroup = db.ProductGroups.Find(id);
            if (productgroup == null)
            {
                return HttpNotFound();
            }
            return View(productgroup);
        }

        // POST: /Admin/ProductGroup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductGroupId,Name,Image,ParentId,Des,CategoryId,IsDefault,CategoryName")] ProductGroup productgroup)
        {
            if (ModelState.IsValid)
            {
                productgroup.Type = 1;
                if (Request.Files[0].ContentLength != 0)
                {
                    if (Request.Files[0].FileName.Contains(".jpg") || Request.Files[0].FileName.Contains(".png")
                    || Request.Files[0].FileName.Contains(".gif"))
                    {
                        string pathToSaveimage = Server.MapPath("/Storedata/Productlist/" + productgroup.ProductGroupId);//Phần vị trí lưu File .
                        CreateFolder(pathToSaveimage);
                        string filename = Path.GetFileName(Request.Files[0].FileName);
                        Request.Files[0].SaveAs(Path.Combine(pathToSaveimage, filename));

                        productgroup.Image = "Storedata/Productlist/" + productgroup.ProductGroupId + "/" + filename;
                        db.SaveChanges();
                    }
                }
                db.Entry(productgroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { categoryId = productgroup.CategoryId });
            }
            return View(productgroup);
        }

        // GET: /Admin/ProductGroup/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroup productgroup = db.ProductGroups.Find(id);
            if (productgroup == null)
            {
                return HttpNotFound();
            }
            db.ProductGroups.Remove(productgroup);
            db.SaveChanges();
            return RedirectToAction("Index", new { categoryId = productgroup.CategoryId });
        }

        // POST: /Admin/ProductGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ProductGroup productgroup = db.ProductGroups.Find(id);
            db.ProductGroups.Remove(productgroup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult SetDefault(int productGroupId)//int productGroupId, int value
        {
            string query = "Update ProductGroups set IsDefault=0; Update ProductGroups set IsDefault=1 where ProductGroupId="+ productGroupId+";" ;
            Cconnect connect = new Cconnect();
            SqlCommand command = new SqlCommand(query, connect.GetConnection());
            command.ExecuteNonQuery();
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public static void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
