using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using foodcourt.Models;

namespace foodcourt.Controllers
{
    [Authorize]
    public class DishController : Controller
    {
        
        private FoodCourtEntities db = new FoodCourtEntities();

        // GET: Dish
        public ActionResult Index()
        {
            return View(db.Dish.ToList());
        }

        // GET: Dish/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dish.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        public ActionResult GetPhoto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dish.Find(id);
            if (dish == null || dish.Photo == null)
            {
                return HttpNotFound();
            }

            return new FileContentResult(dish.Photo, "image/png");
        }

        // GET: Dish/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dish/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,Date")] Dish dish, HttpPostedFileWrapper photo)
        {
            if (ModelState.IsValid)
            {
                if(photo != null)
                {
                    using (var ms = new System.IO.MemoryStream())
                    {
                        photo.InputStream.CopyTo(ms);
                        dish.Photo = ms.ToArray();
                    }
                }

                db.Dish.Add(dish);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dish);
        }

        // GET: Dish/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dish.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // POST: Dish/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price")] Dish dish, HttpPostedFileWrapper photo)
        {
            if (ModelState.IsValid)
            {
                Dish dishFromDb = db.Dish.Find(dish.Id);
                dishFromDb.Name = dish.Name;
                dishFromDb.Description = dish.Description;
                dishFromDb.Price = dish.Price;

                if (photo != null)
                {
                    using (var ms = new System.IO.MemoryStream())
                    {
                        photo.InputStream.CopyTo(ms);
                        dishFromDb.Photo = ms.ToArray();
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dish);
        }

        // GET: Dish/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dish.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // POST: Dish/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dish dish = db.Dish.Find(id);
            db.Dish.Remove(dish);
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
    }
}
