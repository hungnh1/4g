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
    [Authorize]
    public class BannerController : Controller
    {
        private DayGianTuongEntities db = new DayGianTuongEntities();

        // GET: /Admin/Banner/
        public ActionResult Index()
        {
            return View(db.ADVs.ToList());
        }

        // GET: /Admin/Banner/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADV adv = db.ADVs.Find(id);
            if (adv == null)
            {
                return HttpNotFound();
            }
            return View(adv);
        }

        // GET: /Admin/Banner/Create
        public ActionResult Create(int type)
        {
            ViewBag.type = type;
            return View();
        }

        // POST: /Admin/Banner/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdvId,Image,Name,Link,Type,Title,Des")] ADV adv)
        {
            if (ModelState.IsValid)
            {
                db.ADVs.Add(adv);
                db.SaveChanges();
                if (Request.Files[0].ContentLength != 0)
                {
                    if (Request.Files[0].FileName.Contains(".jpg") || Request.Files[0].FileName.Contains(".png")
                    || Request.Files[0].FileName.Contains(".gif"))
                    {
                        string pathToSaveimage = Server.MapPath("/Storedata/ADV/" + adv.AdvId);//Phần vị trí lưu File .
                        CreateFolder(pathToSaveimage);
                        string filename = Path.GetFileName(Request.Files[0].FileName);
                        Request.Files[0].SaveAs(Path.Combine(pathToSaveimage, filename));

                        var update = db.ADVs.ToList().Where(p => p.AdvId == adv.AdvId).First();
                        update.Image = "Storedata/ADV/" + adv.AdvId + "/" + filename;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }

            return View(adv);
        }

        // GET: /Admin/Banner/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADV adv = db.ADVs.Find(id);
            if (adv == null)
            {
                return HttpNotFound();
            }
            return View(adv);
        }

        // POST: /Admin/Banner/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdvId,Image,Name,Link,Type,Title,Des")] ADV adv)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files[0].ContentLength != 0)
                {
                    if (Request.Files[0].FileName.Contains(".jpg") || Request.Files[0].FileName.Contains(".png")
                    || Request.Files[0].FileName.Contains(".gif"))
                    {
                        string pathToSaveimage = Server.MapPath("/Storedata/ADV/" + adv.AdvId);//Phần vị trí lưu File .
                        CreateFolder(pathToSaveimage);
                        string filename = Path.GetFileName(Request.Files[0].FileName);
                        Request.Files[0].SaveAs(Path.Combine(pathToSaveimage, filename));

                        adv.Image = "Storedata/ADV/" + adv.AdvId + "/" + filename;
                        db.SaveChanges();
                    }
                }


                db.Entry(adv).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adv);
        }

        // GET: /Admin/Banner/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADV adv = db.ADVs.Find(id);
            if (adv == null)
            {
                return HttpNotFound();
            }
            db.ADVs.Remove(adv);
            db.SaveChanges();
            return RedirectToAction("Index");
           
        }

        // POST: /Admin/Banner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ADV adv = db.ADVs.Find(id);
            db.ADVs.Remove(adv);
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
