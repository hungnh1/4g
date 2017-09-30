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
namespace baohiem.Areas.Admin.Controllers
{
    public class ProductItemController : Controller
    {
        private Sim4GEntities db = new Sim4GEntities();

        // GET: /Admin/ProductItem/
        public ActionResult Index()
        {
            return View(db.ProductItems.ToList());
        }

        // GET: /Admin/ProductItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductItem productitem = db.ProductItems.Find(id);
            if (productitem == null)
            {
                return HttpNotFound();
            }
            return View(productitem);
        }

        // GET: /Admin/ProductItem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/ProductItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Image,text,Note")] ProductItem productitem)
        {
            if (ModelState.IsValid)
            {
                db.ProductItems.Add(productitem);
                db.SaveChanges();
                if (Request.Files[0].ContentLength != 0)
                {
                    if (Request.Files[0].FileName.Contains(".jpg") || Request.Files[0].FileName.Contains(".png")
                    || Request.Files[0].FileName.Contains(".gif"))
                    {
                        string pathToSaveimage = Server.MapPath("/Storedata/productitem/" + productitem.Id);//Phần vị trí lưu File .
                        CreateFolder(pathToSaveimage);
                        string filename = Path.GetFileName(Request.Files[0].FileName);
                        Request.Files[0].SaveAs(Path.Combine(pathToSaveimage, filename));

                        productitem.Image = "Storedata/productitem/" + productitem.Id + "/" + filename;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }

            return View(productitem);
        }

        // GET: /Admin/ProductItem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductItem productitem = db.ProductItems.Find(id);
            if (productitem == null)
            {
                return HttpNotFound();
            }
            return View(productitem);
        }

        // POST: /Admin/ProductItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Image,text,Note")] ProductItem productitem)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files[0].ContentLength != 0)
                {
                    if (Request.Files[0].FileName.Contains(".jpg") || Request.Files[0].FileName.Contains(".png")
                    || Request.Files[0].FileName.Contains(".gif"))
                    {
                        string pathToSaveimage = Server.MapPath("/Storedata/productitem/" + productitem.Id);//Phần vị trí lưu File .
                        CreateFolder(pathToSaveimage);
                        string filename = Path.GetFileName(Request.Files[0].FileName);
                        Request.Files[0].SaveAs(Path.Combine(pathToSaveimage, filename));

                        productitem.Image = "Storedata/productitem/" + productitem.Id + "/" + filename;
                        db.SaveChanges();
                    }
                }
                db.Entry(productitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productitem);
        }

        // GET: /Admin/ProductItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductItem productitem = db.ProductItems.Find(id);
            if (productitem == null)
            {
                return HttpNotFound();
            }
            db.ProductItems.Remove(productitem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: /Admin/ProductItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductItem productitem = db.ProductItems.Find(id);
            db.ProductItems.Remove(productitem);
            db.SaveChanges();
            return RedirectToAction("Index");
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
