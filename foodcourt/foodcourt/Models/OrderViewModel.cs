using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace foodcourt.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int DishId { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<System.DateTime> Date { get; set; }

        public DishViewModel Dish { get; set; }
    }
}