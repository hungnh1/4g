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
    public class TeamController : Controller
    {
        private Sim4GEntities db = new Sim4GEntities();

        // GET: /Admin/Team/
        public ActionResult Index()
        {
            return View(db.TeamMembers.ToList());
        }

        // GET: /Admin/Team/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamMember teammember = db.TeamMembers.Find(id);
            if (teammember == null)
            {
                return HttpNotFound();
            }
            return View(teammember);
        }

        // GET: /Admin/Team/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Team/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,age,Phone,Email,Adress,Des")] TeamMember teammember)
        {
            if (ModelState.IsValid)
            {
                db.TeamMembers.Add(teammember);
                db.SaveChanges();
                if (Request.Files[0].ContentLength != 0)
                {
                    if (Request.Files[0].FileName.Contains(".jpg") || Request.Files[0].FileName.Contains(".png")
                    || Request.Files[0].FileName.Contains(".gif"))
                    {
                        string pathToSaveimage = Server.MapPath("/Storedata/Team/" + teammember.Id);//Phần vị trí lưu File .
                        CreateFolder(pathToSaveimage);
                        string filename = Path.GetFileName(Request.Files[0].FileName);
                        Request.Files[0].SaveAs(Path.Combine(pathToSaveimage, filename));

                        var update = db.TeamMembers.ToList().Where(p => p.Id == teammember.Id).First();
                        update.Image = "Storedata/Team/" + teammember.Id + "/" + filename;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }

            return View(teammember);
        }

        // GET: /Admin/Team/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamMember teammember = db.TeamMembers.Find(id);
            if (teammember == null)
            {
                return HttpNotFound();
            }
            return View(teammember);
        }

        // POST: /Admin/Team/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TeamMember teammember)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files[0].ContentLength != 0)
                {
                    if (Request.Files[0].FileName.Contains(".jpg") || Request.Files[0].FileName.Contains(".png")
                    || Request.Files[0].FileName.Contains(".gif"))
                    {
                        string pathToSaveimage = Server.MapPath("/Storedata/Team/" + teammember.Id);//Phần vị trí lưu File .
                        CreateFolder(pathToSaveimage);
                        string filename = Path.GetFileName(Request.Files[0].FileName);
                        Request.Files[0].SaveAs(Path.Combine(pathToSaveimage, filename));

                        teammember.Image = "Storedata/Team/" + teammember.Id + "/" + filename;
                       
                    }
                }
                db.Entry(teammember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teammember);
        }

        // GET: /Admin/Team/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamMember teammember = db.TeamMembers.Find(id);
            if (teammember == null)
            {
                return HttpNotFound();
            }
            db.TeamMembers.Remove(teammember);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: /Admin/Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeamMember teammember = db.TeamMembers.Find(id);
            db.TeamMembers.Remove(teammember);
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
