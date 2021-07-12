using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.EF;

namespace ecommerce.Models
{
    [Serializable]
    public class CartItem
    {
        public product product { get; set; }

        public int quantity { get; set; }

        public string color { get; set; }
        public string size { get; set; }
    }
}