using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using foodcourt.Models;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web;

namespace foodcourt.Controllers
{
    public class FoodCourtApiController : ApiController
    {
        private FoodCourtEntities db = new FoodCourtEntities();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: api/FoodCourtApi
        [System.Web.Http.HttpGet]
        public List<DishViewModel> listarPlatos()
        {
            var dishes = new List<DishViewModel>();
            foreach (var dish in db.Dish.ToList())
            {
                DishViewModel dishViewModel = new DishViewModel();
                dishViewModel.Name = dish.Name;
                dishViewModel.Description = dish.Description;
                dishViewModel.Price = dish.Price;
                dishViewModel.Photo = dish.Photo;
                dishes.Add(dishViewModel);
            }
            
            return dishes;
        }

        // GET: api/FoodCourtApi/5
        [ResponseType(typeof(Dish))]
        [System.Web.Http.HttpGet]
        public IHttpActionResult obtenerPlatoPorId(int id)
        {
            Dish dish = db.Dish.Find(id);
            if (dish == null)
            {
                return NotFound();
            }

            return Ok(dish);
        }

        // POST: api/Ordesdsdrs
        [ResponseType(typeof(Order))]
        public IHttpActionResult ordenar(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Order.Add(order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = order.Id }, order);
        }

        //GET: api/Ordesdsdrs
        [System.Web.Http.HttpGet]
        public List<Order> listarOrdenes(string username)
        {
            return db.Order.Where(order => order.UserName == username).ToList();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: api/Ordesdsdrs/5
        [ResponseType(typeof(Order))]
        [System.Web.Http.HttpGet]
        public IHttpActionResult obtenerOrdenPorId(int id)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Ordesdsdrs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult editarOrden(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Ordesdsdrs/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult eliminarOrden(int id)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Order.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }

        // POST: /Account/Login
        [ResponseType(typeof(LoginViewModel))]
        public async Task<IHttpActionResult> login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return Ok(model);
                case SignInStatus.Failure:
                default:
                    return NotFound();
            }
        }

        // POST: /Account/Register
        [ResponseType(typeof(RegisterViewModel))]
        public async Task<IHttpActionResult> registro(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return Ok(model);
                }
                return BadRequest("El registro ha fallado.");
            }

            // If we got this far, something failed, redisplay form
            return BadRequest("Intente de nuevo.");
        }

        private bool DishExists(int id)
        {
            return db.Dish.Count(e => e.Id == id) > 0;
        }

        private bool OrderExists(int id)
        {
            return db.Order.Count(e => e.Id == id) > 0;
        }
    }


}