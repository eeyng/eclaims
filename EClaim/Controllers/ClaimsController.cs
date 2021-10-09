using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EClaim.Controllers
{
    public class ClaimsController : Controller
    {
        public ActionResult ClaimListing()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Home", "Login");
            }
            return View();
        }
    }
}