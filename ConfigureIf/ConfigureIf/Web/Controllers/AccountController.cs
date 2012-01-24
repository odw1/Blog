using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Plumbing;

namespace Web.Controllers
{
    [RequiresAudit]
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
