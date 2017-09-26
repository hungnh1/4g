using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EnF;

namespace baohiem.Controllers
{
    public class NhomSanPhamController : Controller
    {
        private DayGianTuongEntities db = new DayGianTuongEntities();

        // GET: /NhomSanPham/
        public ActionResult Index()
        {
            return View(db.ProductGroups.ToList());
        }

        // GET: /NhomSanPham/Details/5
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

        // GET: /NhomSanPham/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /NhomSanPham/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ProductGroupId,Name,Image,ParentId,Des,CategoryId,IsDefault,CategoryName")] ProductGroup productgroup)
        {
            if (ModelState.IsValid)
            {
                db.ProductGroups.Add(productgroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productgroup);
        }

        // GET: /NhomSanPham/Edit/5
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

        // POST: /NhomSanPham/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ProductGroupId,Name,Image,ParentId,Des,CategoryId,IsDefault,CategoryName")] ProductGroup productgroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productgroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productgroup);
        }

        // GET: /NhomSanPham/Delete/5
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
            return View(productgroup);
        }

        // POST: /NhomSanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ProductGroup productgroup = db.ProductGroups.Find(id);
            db.ProductGroups.Remove(productgroup);
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
    }
}
