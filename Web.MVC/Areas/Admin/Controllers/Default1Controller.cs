using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace baohiem.Areas.Admin.Controllers
{
    [Authorize]
    public class Default1Controller : Controller
    {
        //
        // GET: /Admin/Default1/
        public ActionResult Index()
        {
            return View();
        }
	}
}