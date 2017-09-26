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
using System.Configuration;
namespace baohiem.Areas.Admin.Controllers
{
   //  [baohiem.App_Start.ActionAuthor(NotifyUrl = "~/Account/Login")]
    public class ConfigController : Controller
    {
        private DayGianTuongEntities db = new DayGianTuongEntities();
        static string strConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnectionCTm"].ConnectionString;
        /// <summary>
        /// Global SQL server connection
        /// </summary>
        public static SqlConnection connection;

        public static SqlConnection GetConnection()
        {
            if (connection == null) { connection = new SqlConnection(strConStr); }
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            return connection;
        }

        // GET: /Admin/Config/
        public ActionResult Index()
        {
            return View(db.Configs.ToList());
        }

        public ActionResult Details()
        {
            return View();
        }
        // GET: /Admin/Config/Details/5
        [HttpPost]
        public ActionResult Details(FormCollection fc)
        {
            //SqlConnection cnn = new SqlConnection(Cconnect.ConnectStrting());
            //cnn.Open();
            //SqlCommand command = new SqlCommand(sql, cnn);
            //return command.ExecuteNonQuery();
            SqlCommand command = new SqlCommand(fc["txtip"], GetConnection());
            command.ExecuteNonQuery();
            return View("Details");
           
        }

        // GET: /Admin/Config/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Config/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConfigId,Keyword,Domain,HotLine,Social,EmailSent,MailPass,PageTitle,GooleMap,ShowPageId,Youtube,Sky,Yahoo,Mail,Slogan,Address,Hotline1,Hotline2,Yahoo1,Yahoo2,Sky1,Sky2,MailRetrive,Facebook,Google,Twiter,Metadata,Description,CCmail3,CCmail4")] Config config)
        {
            if (ModelState.IsValid)
            {
                db.Configs.Add(config);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(config);
        }

        // GET: /Admin/Config/Edit/5
        [baohiem.App_Start.ActionAuthor(NotifyUrl = "~/Account/Login")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Config config = db.Configs.Find(id);
            if (config == null)
            {
                return HttpNotFound();
            }
            return View(config);
        }

        // POST: /Admin/Config/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "ConfigId,Keyword,Domain,HotLine,Social,EmailSent,MailPass,PageTitle,GooleMap,ShowPageId,Youtube,Sky,Yahoo,Mail,Slogan,Address,Hotline1,Hotline2,Yahoo1,Yahoo2,Sky1,Sky2,MailRetrive,Facebook,Google,Twiter,Metadata,Description,CCmail3,CCmail4")] Config config)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files[0].ContentLength != 0)
                {
                    if (Request.Files[0].FileName.Contains(".jpg") || Request.Files[0].FileName.Contains(".png")
                    || Request.Files[0].FileName.Contains(".gif"))
                    {
                        string pathToSaveimage = Server.MapPath("/Storedata/config/" + config.ConfigId);//Phần vị trí lưu File .
                        CreateFolder(pathToSaveimage);
                        string filename = Path.GetFileName(Request.Files[0].FileName);
                        Request.Files[0].SaveAs(Path.Combine(pathToSaveimage, filename));

                        config.Slogan = "Storedata/config/" + config.ConfigId + "/" + filename;
                        db.SaveChanges();
                    }
                }
                db.Entry(config).State = EntityState.Modified;
                db.SaveChanges();

            }
            return View(config);
        }
        public static void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        // GET: /Admin/Config/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Config config = db.Configs.Find(id);
            if (config == null)
            {
                return HttpNotFound();
            }
            return View(config);
        }

        // POST: /Admin/Config/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Config config = db.Configs.Find(id);
            db.Configs.Remove(config);
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
