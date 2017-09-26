using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EnF;
using System.Web.Security;
using System.Security.Principal;
using Web.Repository;
namespace baohiem.Areas.Admin.Controllers
{
    [Authorize]
    public class UserRoleController : Controller
    {
        private DayGianTuongEntities db = new DayGianTuongEntities();

        // GET: /Admin/UserRole/
        public ActionResult Index()
        {
            if (!UserRepository.CheckAdminRole(User.Identity.Name))
            {
                ViewBag.messenge = "Bạn không có quyền ở trang này";
                return View("ErroPage");
            }
            string currentUserName = User.Identity.Name;
            return View(db.UserRoles.Where(p => p.UserName != currentUserName));
        }

        // GET: /Admin/UserRole/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userrole = db.UserRoles.Find(id);
            if (userrole == null)
            {
                return HttpNotFound();
            }
            return View(userrole);
        }

        // GET: /Admin/UserRole/Create
        public ActionResult Create()
        {
            if (!UserRepository.CheckAdminRole(User.Identity.Name))
            {
                ViewBag.messenge = "Bạn không có quyền ở trang này";
                return View("ErroPage");
            }
            List<SelectListItem> SelectLisrole = new List<SelectListItem>();
            SelectLisrole.Add(new SelectListItem { Text = "User", Value = "2", Selected = true });  //, 
            SelectLisrole.Add(new SelectListItem { Text = "Admin", Value = "1" });
            ViewBag.Role = SelectLisrole;
            //ViewBag.thu = new SelectList(new[] { "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy", "Chủ nhật" }
            //  .Select(x => new { value = x, text = x }),
            //  "value", "text", 0);
            return View();
        }

        // POST: /Admin/UserRole/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,PassWord,Birthday,Phone,AreaId,Role,Name,gender")] UserRole userrole)
        {

            var isvalid = db.UserRoles.Where(p => p.UserName == userrole.UserName).ToList();
            if (isvalid.Count > 0)
            {
                return View(userrole);
            }

            if (ModelState.IsValid)
            {
                string pass = FormsAuthentication.HashPasswordForStoringInConfigFile(userrole.PassWord, "MD5");
                userrole.PassWord = pass;
                db.UserRoles.Add(userrole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userrole);
        }

        // GET: /Admin/UserRole/Edit/5
        public ActionResult Edit(long? id)
        {
            if (!UserRepository.CheckAdminRole(User.Identity.Name))
            {
                ViewBag.messenge = "Bạn không có quyền ở trang này";
                return View("ErroPage");
            }
            List<SelectListItem> SelectLisrole = new List<SelectListItem>();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userrole = db.UserRoles.Find(id);
            if (userrole.Role == 1)
            {
                SelectLisrole.Add(new SelectListItem { Text = "User", Value = "2" });  //, 
                SelectLisrole.Add(new SelectListItem { Text = "Admin", Value = "1", Selected = true });
                ViewBag.Role = SelectLisrole;
            }
            else
            {
                SelectLisrole.Add(new SelectListItem { Text = "User", Value = "2", Selected = true });  //, 
                SelectLisrole.Add(new SelectListItem { Text = "Admin", Value = "1" });
                ViewBag.Role = SelectLisrole;
            }
            if (userrole == null)
            {
                return HttpNotFound();
            }
            return View(userrole);
        }

        // POST: /Admin/UserRole/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserRole userrole)
        {
            //var isvalid = db.UserRoles.Where(p => p.UserName == userrole.UserName).ToList();
            //if (isvalid.Count > 0)
            //{
            //    return View(userrole);
            //}
            if (ModelState.IsValid)
            {
                //  string pass = FormsAuthentication.HashPasswordForStoringInConfigFile(userrole.PassWord, "MD5");
                //userrole.PassWord = pass;
                db.Entry(userrole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userrole);
        }

        public ActionResult ChangePass()
        {
            List<SelectListItem> SelectLisrole = new List<SelectListItem>();
            SelectLisrole.Add(new SelectListItem { Text = "User", Value = "2", Selected = true });  //, 
            SelectLisrole.Add(new SelectListItem { Text = "Admin", Value = "1" });
            ViewBag.Role = SelectLisrole;
            string currentUserName = User.Identity.Name;
            var curUser = db.UserRoles.Where(p => p.UserName == currentUserName).First();


            baohiem.Models.UserRoleModel user = new Models.UserRoleModel();
            user.UserName = curUser.UserName;

            return View(user);
        }
        [HttpPost]
        public ActionResult ChangePass(baohiem.Models.UserRoleModel user)
        {
            if (ModelState.IsValid)
            {
            string pass = FormsAuthentication.HashPasswordForStoringInConfigFile(user.OldPassword, "MD5");
            string currentUserName = User.Identity.Name;
            var currentUser = db.UserRoles.Where(p => p.UserName == currentUserName).First();
            if (pass != currentUser.PassWord)
            {
                TempData["validpass"] = "(*)Mật khẩu củ không đúng";
                return View(user);
            }
           
                currentUser.PassWord = pass;
                db.SaveChanges();
                TempData["success"] = "Đổi mật khẩu thành công";
            
            user.UserName = User.Identity.Name;
            // If we got this far, something failed, redisplay form
            }
            return View(user);
        }
        // GET: /Admin/UserRole/Delete/5
        public ActionResult Delete(long? id)
        {
            if (!UserRepository.CheckAdminRole(User.Identity.Name))
            {
                ViewBag.messenge = "Bạn không có quyền ở trang này";
                return View("ErroPage");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userrole = db.UserRoles.Find(id);
            if (userrole == null)
            {
                return HttpNotFound();
            }

            db.UserRoles.Remove(userrole);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: /Admin/UserRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            UserRole userrole = db.UserRoles.Find(id);
            db.UserRoles.Remove(userrole);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ErroPage()
        {
           
            return View();
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
