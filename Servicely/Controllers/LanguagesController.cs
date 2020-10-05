using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Servicely.Controllers
{
    public class LanguagesController : Controller
    {
        // GET: Base
        public ActionResult Arabic()
        {
            Session["flagIcon"] = "egypt-flag-round-icon-32.png";
            Session["lang"] = "ar-EG";
            return Redirect(Request.UrlReferrer.ToString());
        }


        public ActionResult English()
        {
            Session["flagIcon"] = "united-states-of-america-flag-round-icon-32.png";
            Session["lang"] = "en-US";
            return Redirect(Request.UrlReferrer.ToString());
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }
}