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
    public class RestaurantDishController : Controller
    {
        private FoodCourtEntities db = new FoodCourtEntities();

        // GET: RestaurantDish
        public ActionResult Index()
        {
            var restaurantDishes = db.RestaurantDishes.Include(r => r.Dish).Include(r => r.Restaurant);
            return View(restaurantDishes.ToList());
        }

        // GET: RestaurantDish/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantDish restaurantDish = db.RestaurantDishes.Find(id);
            if (restaurantDish == null)
            {
                return HttpNotFound();
            }
            return View(restaurantDish);
        }

        // GET: RestaurantDish/Create
        public ActionResult Create()
        {
            ViewBag.DishId = new SelectList(db.Dishes, "Id", "Name");
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name");
            return View();
        }

        // POST: RestaurantDish/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RestaurantId,DishId")] RestaurantDish restaurantDish)
        {
            if (ModelState.IsValid)
            {
                db.RestaurantDishes.Add(restaurantDish);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DishId = new SelectList(db.Dishes, "Id", "Name", restaurantDish.DishId);
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", restaurantDish.RestaurantId);
            return View(restaurantDish);
        }

        // GET: RestaurantDish/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantDish restaurantDish = db.RestaurantDishes.Find(id);
            if (restaurantDish == null)
            {
                return HttpNotFound();
            }
            ViewBag.DishId = new SelectList(db.Dishes, "Id", "Name", restaurantDish.DishId);
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", restaurantDish.RestaurantId);
            return View(restaurantDish);
        }

        // POST: RestaurantDish/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RestaurantId,DishId")] RestaurantDish restaurantDish)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurantDish).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DishId = new SelectList(db.Dishes, "Id", "Name", restaurantDish.DishId);
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", restaurantDish.RestaurantId);
            return View(restaurantDish);
        }

        // GET: RestaurantDish/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantDish restaurantDish = db.RestaurantDishes.Find(id);
            if (restaurantDish == null)
            {
                return HttpNotFound();
            }
            return View(restaurantDish);
        }

        // POST: RestaurantDish/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RestaurantDish restaurantDish = db.RestaurantDishes.Find(id);
            db.RestaurantDishes.Remove(restaurantDish);
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
