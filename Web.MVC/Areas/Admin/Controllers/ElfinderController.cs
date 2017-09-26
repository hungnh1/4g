using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace baohiem.Areas.Admin.Controllers
{
    public class ElfinderController : Controller
    {
        //
        // GET: /Admin/Elfinder/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Popup()
        {
            return View();
        }
	}
}