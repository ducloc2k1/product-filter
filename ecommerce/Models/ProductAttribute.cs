using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecommerce.Models
{
    public class ProductAttribute
    {
        public Model.EF.product product { get; set; }

        public string[] size { get; set; }

        public List<string> listColorValue { get; set; }

        public List<string> listColorCode { get; set; }

        public List<string> listGallery { get; set; }

        public double salePercent;
    }
}