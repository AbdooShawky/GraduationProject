﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Servicely.Controllers
{
    public class ATMCivilAffairsController : Controller
    {
        // GET: ATMCivilAffairs
        public ActionResult Index()
        {
            return View();
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }
}