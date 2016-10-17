using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Web.Controllers
{
    [Authorize]
    public class BD03Controller : Controller
    {
        //
        // GET: /BD03/
        public ActionResult Index()
        {

            return View();
        }
	}
}