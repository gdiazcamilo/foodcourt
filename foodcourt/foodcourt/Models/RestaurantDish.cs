//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace foodcourt.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RestaurantDish
    {
        public int Id { get; set; }
        public Nullable<int> RestaurantId { get; set; }
        public Nullable<int> DishId { get; set; }
    
        public virtual Dish Dish { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}