using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using foodcourt.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
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
        public List<DishViewModel> GetDishes()
        {
            var dishes = new List<DishViewModel>();
            foreach (var dish in db.Dish.ToList())
            {
                DishViewModel dishViewModel = new DishViewModel();
                dishViewModel.Name = dish.Name;
                dishViewModel.Description = dish.Description;
                dishViewModel.Price = dish.Price;
                dishViewModel.Photo = dish.Photo;
                dishViewModel.Id = dish.Id;
                dishes.Add(dishViewModel);
            }
            
            return dishes;
        }

        // GET: api/FoodCourtApi/5
        [ResponseType(typeof(Dish))]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetDishById(int id)
        {
            Dish dish = db.Dish.Find(id);
            DishViewModel dishViewModel = new DishViewModel();
            dishViewModel.Name = dish.Name;
            dishViewModel.Description = dish.Description;
            dishViewModel.Price = dish.Price;
            dishViewModel.Photo = dish.Photo;
            dishViewModel.Id = dish.Id;
            if (dish == null)
            {
                return NotFound();
            }

            return Ok(dishViewModel);
        }

        [System.Web.Http.HttpGet]
        [ResponseType(typeof(object))]
        public object Order(string username, int dishId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order order = new Order {
                Date= DateTime.Now,
                State = 0,
                UserName=username,
                DishId=dishId
            };

            db.Order.Add(order);
           int result = db.SaveChanges();
            if(result > 0)
            {
                return new { Response = true };
            }
            return new { Response = false };
        }

        //GET: api/Ordesdsdrs
        [System.Web.Http.HttpGet]
        public List<OrderViewModel> GetOrders(string username)
        {
            var orders = new List<OrderViewModel>();
            foreach (var order in db.Order.Where(order => order.UserName == username).ToList())
            {
                OrderViewModel orderViewModel = new OrderViewModel();
                orderViewModel.DishId = order.DishId;
                orderViewModel.Id = order.Id;
                orderViewModel.State = order.State;
                orderViewModel.UserName = order.UserName;
                orderViewModel.Date = order.Date;

                DishViewModel dish = new DishViewModel();
                dish.Id = order.Dish.Id;
                dish.Name = order.Dish.Name;
                dish.Description = order.Dish.Description;
                dish.Price = order.Dish.Price;
                dish.Photo = order.Dish.Photo;

                orderViewModel.Dish = dish;
                orders.Add(orderViewModel);
            }
            
            return orders;
        }

         // GET: api/Ordesdsdrs/5
        [ResponseType(typeof(Order))]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetOrderById(int id)
        {
            Order order = db.Order.Find(id);
            OrderViewModel orderViewModel = new OrderViewModel();
            orderViewModel.DishId = order.DishId;
            orderViewModel.Id = order.Id;
            orderViewModel.State = order.State;
            orderViewModel.UserName = order.UserName;
            orderViewModel.Date = order.Date;

            DishViewModel dish = new DishViewModel();
            dish.Id = order.Dish.Id;
            dish.Name = order.Dish.Name;
            dish.Description = order.Dish.Description;
            dish.Price = order.Dish.Price;
            dish.Photo = order.Dish.Photo;

            orderViewModel.Dish = dish;

            if (order == null)
            {
                return NotFound();
            }

            return Ok(orderViewModel);
        }

        [ResponseType(typeof(object))]
        [System.Web.Http.HttpGet]
        public object ReceiveOrder(int id)
        {
            Order order = db.Order.Find(id);

            if (order == null)
            {
                return new { Response = false };
            }

            order.State = 2; //recibido
            int result = db.SaveChanges();

            if(result > 0)
            {
                return new { Response = true };
            }
            return new { Response = false };
        }

        // PUT: api/Ordesdsdrs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult EditOrder(int id, Order order)
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
        public IHttpActionResult DeleteOrder(int id)
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

        [System.Web.Http.HttpGet]
        public async Task<object> Login(string email, string password)
        {

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(email, password, true, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return new { Response = true};
                case SignInStatus.Failure:
                default:
                    return new { Response = false };
            }
        }

        [System.Web.Http.HttpGet]
        public async Task<object> Register(string email, string password)
        {
           
            RegisterViewModel model = new RegisterViewModel {
                Email = email,
                Password = password
            };
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                return new { Response = true }; 
            }
            return new { Response = false };
           
        }

        private bool DishExists(int id)
        {
            return db.Dish.Count(e => e.Id == id) > 0;
        }

        private bool OrderExists(int id)
        {
            return db.Order.Count(e => e.Id == id) > 0;
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