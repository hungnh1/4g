using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EnF;
using Microsoft.Owin.Security;
namespace baohiem.Areas.Admin.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private DayGianTuongEntities db = new DayGianTuongEntities();

        // GET: /Admin/User/
        public ActionResult Index()
        {
           
            return View(db.AspNetUsers.Where(p => p.UserName != User.Identity.Name).ToList());
        }

        // GET: /Admin/User/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspnetuser = db.AspNetUsers.Find(id);
            if (aspnetuser == null)
            {
                return HttpNotFound();
            }
            return View(aspnetuser);
        }

        // GET: /Admin/User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,UserName,PasswordHash,SecurityStamp,Discriminator")] AspNetUser aspnetuser)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUsers.Add(aspnetuser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aspnetuser);
        }

        // GET: /Admin/User/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspnetuser = db.AspNetUsers.Find(id);
            if (aspnetuser == null)
            {
                return HttpNotFound();
            }
            return View(aspnetuser);
        }

        // POST: /Admin/User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,UserName,PasswordHash,SecurityStamp,Discriminator")] AspNetUser aspnetuser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspnetuser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspnetuser);
        }

        // GET: /Admin/User/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspnetuser = db.AspNetUsers.Find(id);
            if (aspnetuser == null)
            {
                return HttpNotFound();
            }
            db.AspNetUsers.Remove(aspnetuser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: /Admin/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUser aspnetuser = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspnetuser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Thoat()
        {
            AuthenticationManager.SignOut();
            return Redirect("/Account/Login");
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
