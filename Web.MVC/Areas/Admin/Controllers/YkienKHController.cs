using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EnF;

namespace baohiem.Areas.Admin.Controllers
{
    [Authorize]
    public class YkienKHController : Controller
    {
        private Sim4GEntities db = new Sim4GEntities();
                   
        // GET: /Admin/YkienKH/
        public ActionResult Index()
        {
           
            return View(db.CustommerOpinions.ToList());
        }

        // GET: /Admin/YkienKH/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustommerOpinion custommeropinion = db.CustommerOpinions.Find(id);
            if (custommeropinion == null)
            {
                return HttpNotFound();
            }
            return View(custommeropinion);
        }

        // GET: /Admin/YkienKH/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/YkienKH/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,Email,Phone,opinion")] CustommerOpinion custommeropinion)
        {
            if (ModelState.IsValid)
            {
                db.CustommerOpinions.Add(custommeropinion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(custommeropinion);
        }

        // GET: /Admin/YkienKH/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustommerOpinion custommeropinion = db.CustommerOpinions.Find(id);
            if (custommeropinion == null)
            {
                return HttpNotFound();
            }
            return View(custommeropinion);
        }

        // POST: /Admin/YkienKH/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,Email,Phone,opinion")] CustommerOpinion custommeropinion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(custommeropinion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(custommeropinion);
        }

        // GET: /Admin/YkienKH/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustommerOpinion custommeropinion = db.CustommerOpinions.Find(id);
            if (custommeropinion == null)
            {
                return HttpNotFound();
            }
                  
            db.CustommerOpinions.Remove(custommeropinion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: /Admin/YkienKH/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustommerOpinion custommeropinion = db.CustommerOpinions.Find(id);
            db.CustommerOpinions.Remove(custommeropinion);
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
