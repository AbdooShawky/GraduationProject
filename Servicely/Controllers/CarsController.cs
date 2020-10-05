using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Servicely.Models;

namespace Servicely.Controllers
{
    public class CarsController : BaseController
    {
        private DbMasterEntities1 db = new DbMasterEntities1();

        // GET: Cars
        public ActionResult Index()
        {
            return View(db.Cars.Where(a=> a.Is_Deleted!= true).ToList());
        }


        // GET: Cars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      
        public ActionResult Create( Car car)
        {
            if (ModelState.IsValid)
            {
                var data = db.Cars.Where(a => a.Is_Deleted != true && a.CarName == car.CarName).SingleOrDefault();
                if(data != null)
                {
                    ViewBag.ErrMessage = Languages.Language.NameAlreadyExist;
                    return View(car);
                }
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(car);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
    
        public ActionResult Edit( Car car)
        {
            if (ModelState.IsValid)
            {
                var data = db.Cars.Where(a => a.Is_Deleted != true && a.Id != car.Id);

                foreach (var item in data)
                {
                    if( item.CarName == car.CarName)
                    {
                        ViewBag.ErrMessage = Languages.Language.NameAlreadyExist;
                        return View(car);

                    }


                }

                var old = db.Cars.Find(car.Id);
                old.CarName = car.CarName;
                old.CarNameArabic = car.CarNameArabic;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = db.Cars.Find(id);
            car.Is_Deleted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            //your handling logic here
            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("errorpage", "home");
        }
    }
}
