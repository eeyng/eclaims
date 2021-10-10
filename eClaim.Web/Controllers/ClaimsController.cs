using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eClaim.Web.Controllers
{
    public class ClaimsController : Controller
    {
        public ActionResult ClaimListing()
        {
            return View();
        }

        public ActionResult ClaimForm()
        {
            return View();
        }
    }
}