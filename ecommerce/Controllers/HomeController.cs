using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using ecommerce.Models;
using Newtonsoft.Json;

namespace ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private Model.Dao.ProductDao productDao = new Model.Dao.ProductDao();

        public ActionResult Index()
        {

            var products = productDao.getAllProduct();

            List<ProductAttribute> listProductAttribute = new List<ProductAttribute>();

            foreach (var product in products)
            {
                var productAttr = new ProductAttribute();

                productAttr.product = product;

                productAttr.size = productDao
                    .getAttributeByIdProduct(product.id)
                    .Where(x => String.Equals(x.name, "size"))
                    .Select(x => x.attribute_value).ToArray();

                //tạo danh sách color value và color code
                productAttr.listColorValue = new List<string>();

                productAttr.listColorCode = new List<string>();

                productDao
                    .getAttributeByIdProduct(product.id)
                    .ForEach(x =>
                    {
                        if (String.Equals(x.name, "color"))
                        {
                            productAttr.listColorCode.Add(x.code);

                            productAttr.listColorValue.Add(x.attribute_value);
                        }
                    });

                var images = XElement.Parse(product.gallery);

                productAttr.listGallery = new List<string>();

                foreach (var image in images.Elements())
                {
                    productAttr.listGallery.Add(image.Value);
                }

                //sale percent
                var percent = ((product.price - product.sale_price) / product.price) * 100;

                productAttr.salePercent = Math.Ceiling(Convert.ToDouble(percent.Value));

                listProductAttribute.Add(productAttr);
            }

            return View(listProductAttribute);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}