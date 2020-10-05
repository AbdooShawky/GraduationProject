using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;
using System.IO;
namespace Servicely.Controllers
{
    public class CitizenPhotosController : Controller
    {
        DbMasterEntities1 db = new DbMasterEntities1();

        // GET: CitizenPhotos
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Photo p ,HttpPostedFileBase f1)
        {
          ;
            
        
            string pName =Guid.NewGuid() +  Path.GetFileName( f1.FileName); //Name of photo only
            string pPath = Server.MapPath( "~/photos/" +pName);
            string pPathName = Path.Combine(pName , pPath);
            f1.SaveAs(pPathName);

            p.Photo_Url = pName;
            db.Photos.Add(p);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }
}