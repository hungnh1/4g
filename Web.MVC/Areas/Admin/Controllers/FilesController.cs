using System.Web.Mvc;
using System.IO;
using ElFinder;
using System.Configuration;
namespace baohiem.Areas.Admin.Controllers
{
    public class FilesController : Controller
    {
        //
        // GET: /Admin/Files/
        private Connector _connector;

        public Connector Connector
        {
            get
            {
                if (_connector == null)
                {
                    FileSystemDriver driver = new FileSystemDriver();
                    DirectoryInfo thumbsStorage = new DirectoryInfo(Server.MapPath("/Uploads"));
                    driver.AddRoot(new Root(new DirectoryInfo(Server.MapPath("/Uploads")), "/Uploads/")
                    {
                        StartPath = new DirectoryInfo(Server.MapPath("/Uploads")),
                        ThumbnailsStorage = thumbsStorage,
                        MaxUploadSizeInMb = 2.2,
                        ThumbnailsUrl = "/Thumbnails/"
                       // ThumbnailsUrl = "/Files/Thumbs/"

                    });
                    _connector = new Connector(driver);
                }
                return _connector;
            }
        }
        //public Connector Connector
        //{
        //    get
        //    {
        //        if (_connector == null)
        //        {
        //            FileSystemDriver driver = new FileSystemDriver();
        //            DirectoryInfo thumbsStorage = new DirectoryInfo(Server.MapPath("~/Files"));
        //            driver.AddRoot(new Root(new DirectoryInfo(@"C:\Program Files"))
        //            {
        //                IsLocked = true,
        //                IsReadOnly = true,
        //                IsShowOnly = true,
        //                ThumbnailsStorage = thumbsStorage,
        //                ThumbnailsUrl = "Thumbnails/"
        //            });
        //            driver.AddRoot(new Root(new DirectoryInfo(Server.MapPath("~/Files")), "/Files/")
        //            {
        //                Alias = "My documents",
        //                StartPath = new DirectoryInfo(Server.MapPath("~/Files/новая папка")),
        //                ThumbnailsStorage = thumbsStorage,
        //                MaxUploadSizeInMb = 2.2,
        //                ThumbnailsUrl = "Thumbnails/"
        //            });
        //            _connector = new Connector(driver);
        //        }
        //        return _connector;
        //    }
        //}
        public ActionResult Index()
        {
            return Connector.Process(this.HttpContext.Request);
        }

        //public virtual ActionResult SelectFile(string target)
        //{
        //    FileSystemDriver driver = new FileSystemDriver();

        //    driver.AddRoot(
        //        new Root(
        //            new DirectoryInfo(Server.MapPath("~/Uploads")),
        //            "http://" + Request.Url.Authority + "/Uploads") { IsReadOnly = false });

        //    var connector = new Connector(driver);

        //    return Json(connector.GetFileByHash(target).FullName);
        //}
        public ActionResult SelectFile(string target)
        {
            return Json(Connector.GetFileByHash(target).FullName);
        }

        public ActionResult Thumbs(string tmb)
        {
            return Connector.GetThumbnail(Request, Response, tmb);
        }
	}
}