using EnF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace baohiem.Areas.Admin.Controllers
{
    public class GroupNewsController : Controller
    {
        private Sim4GEntities db = new Sim4GEntities();
        // GET: Admin/GroupNews
        public ActionResult Index()
        {
            return View(db.Pages.Where(p=>p.ParentId==null|| p.ParentId == 0).OrderBy(o=>o.Pos).ToList());
        }

        // GET: Admin/GroupNews/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Pos")] Page page)
        {
            if (ModelState.IsValid)
            {
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(page);
        }


        // GET: Admin/GroupNews/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = db.Pages.Find(id);
            if (page == null)
            {
                return HttpNotFound();
            }

            if(page.Pos!=null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(page);
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PageId,Name,PContent,Recap,Link,Image,ParentId,Pos")] Page page)
        {
            if (ModelState.IsValid)
            {             
                db.Entry(page).State = EntityState.Modified;

                db.SaveChanges();
            
                return RedirectToAction("Index");
            }
            return View(page);
        }


        // GET: Admin/GroupNews/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = db.Pages.Find(id);
            if (page == null)
            {
                return HttpNotFound();
            }

            db.Pages.Remove(page);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Admin/GroupNews/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
