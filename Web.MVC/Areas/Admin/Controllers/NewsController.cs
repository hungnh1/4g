using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EnF;
using Web.Repository;
using System.IO;
namespace baohiem.Areas.Admin.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        private Sim4GEntities db = new Sim4GEntities();

        // GET: /Admin/News/
        public ActionResult Index(int newId)
        {
            return View(db.Pages.Where(p=>p.ParentId== newId).ToList());
        }
        public ActionResult About()
        {
            Page rs = null;
            if (db.Pages.ToList().Count>0)
            {
                 rs = db.Pages.OrderBy(p=>p.PageId).Skip(0).Take(1).First();
                
            }
            return View(rs);
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult About(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Link = "/Tin-tuc/" + ClearWordRepository.NoVNeseLower(page.Name) + "-" + page.PageId;
                if (page.PageId != 0)
                {
                    if (Request.Files[0].ContentLength != 0)
                    {
                        if (Request.Files[0].FileName.Contains(".jpg") || Request.Files[0].FileName.Contains(".png")
                        || Request.Files[0].FileName.Contains(".gif"))
                        {
                            string pathToSaveimage = Server.MapPath("/Storedata/news/" + page.PageId);//Phần vị trí lưu File .
                            CreateFolder(pathToSaveimage);
                            string filename = Path.GetFileName(Request.Files[0].FileName);
                            Request.Files[0].SaveAs(Path.Combine(pathToSaveimage, filename));

                            page.Image = "Storedata/news/" + page.PageId + "/" + filename;
                            db.SaveChanges();
                        }
                    }
                    db.Entry(page).State = EntityState.Modified;

                    db.SaveChanges();
                }
                else
                {
                    db.Pages.Add(page);
                    db.SaveChanges();


                    if (Request.Files[0].ContentLength != 0)
                    {
                        if (Request.Files[0].FileName.Contains(".jpg") || Request.Files[0].FileName.Contains(".png")
                        || Request.Files[0].FileName.Contains(".gif"))
                        {
                            string pathToSaveimage = Server.MapPath("/Storedata/news/" + page.PageId);//Phần vị trí lưu File .
                            CreateFolder(pathToSaveimage);
                            string filename = Path.GetFileName(Request.Files[0].FileName);
                            Request.Files[0].SaveAs(Path.Combine(pathToSaveimage, filename));

                            var update = db.Pages.ToList().Where(p => p.PageId == page.PageId).First();
                            update.Image = "Storedata/news/" + page.PageId + "/" + filename;
                            db.SaveChanges();
                        }
                    }
                }


              



                return RedirectToAction("Index");
            }

            return View(page);
        }

              
        // GET: /Admin/News/Details/5
        public ActionResult Details(long? id)
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
            return View(page);
        }

        // GET: /Admin/News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PageId,Name,PContent,Recap,Link,Image,ParentId,Pos")] Page page)
        {
            if (ModelState.IsValid)
            {
               
                var url = ClearWordRepository.NoVNeseLower(page.Name).Replace("?", "");
                db.Pages.Add(page);
                db.SaveChanges();
                page.Link = "/Tin-tuc/" + url + "-" + page.PageId;

                if (Request.Files[0].ContentLength != 0)
                {
                    if (Request.Files[0].FileName.Contains(".jpg") || Request.Files[0].FileName.Contains(".png")
                    || Request.Files[0].FileName.Contains(".gif"))
                    {
                        string pathToSaveimage = Server.MapPath("/Storedata/news/" + page.PageId);//Phần vị trí lưu File .
                        CreateFolder(pathToSaveimage);
                        string filename = Path.GetFileName(Request.Files[0].FileName);
                        Request.Files[0].SaveAs(Path.Combine(pathToSaveimage, filename));

                        var update = db.Pages.ToList().Where(p => p.PageId == page.PageId).First();
                        update.Image = "Storedata/news/" + page.PageId + "/" + filename;
                       
                    }
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(page);
        }

        // GET: /Admin/News/Edit/5
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
            return View(page);
        }

        // POST: /Admin/News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PageId,Name,PContent,Recap,Link,Image,ParentId,Pos")] Page page)
        {
            if (ModelState.IsValid)
            {
                page.Link = "/Tin-tuc/" + ClearWordRepository.NoVNeseLower(page.Name) + "-" + page.PageId;
                page.Link = page.Link.Replace("?", "");
                if (Request.Files.Count != 0)
                {
                    if (Request.Files[0].FileName.Contains(".jpg") || Request.Files[0].FileName.Contains(".png")
                    || Request.Files[0].FileName.Contains(".gif"))
                    {
                        string pathToSaveimage = Server.MapPath("/Storedata/news/" + page.PageId);//Phần vị trí lưu File .
                        CreateFolder(pathToSaveimage);
                        string filename = Path.GetFileName(Request.Files[0].FileName);
                        Request.Files[0].SaveAs(Path.Combine(pathToSaveimage, filename));


                        page.Image = "Storedata/news/" + page.PageId + "/" + filename;

                    }
                }

                db.Entry(page).State = EntityState.Modified;

                db.SaveChanges();
                if(page.PageId==db.Pages.First().PageId)
                {
                    return Redirect("/Admin/News/Edit/" + page.PageId);
                }
                return RedirectToAction("Index");
            }
            return View(page);
        }

        // GET: /Admin/News/Delete/5
        public ActionResult Delete(long? id)
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

        // POST: /Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Page page = db.Pages.Find(id);
            db.Pages.Remove(page);
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
