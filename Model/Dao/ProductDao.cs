using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.EF;


namespace Model.Dao
{
    public class ProductDao
    {
        private product_has_attributeEntities db = new product_has_attributeEntities();

        public List<product> filterProduct()
        {
            db.Configuration.ProxyCreationEnabled = false;

            try
            {
                List<product> lstFilter = db.products.ToList();

                var productWithAttr = db.getProductWithAttribute().ToList();

                //Filter size 
                string filter_size = HttpContext.Current.Request?.Params["filter_size"];

                var lstFilter_size = String.IsNullOrEmpty(filter_size) ? new string[0] : filter_size.Split(',');

                if (lstFilter_size.Length > 0)
                {
                    lstFilter = lstFilter.Where(p =>
                    {
                        var productAttributes = db.getAttributeProduct(p.id).Select(x => x.attribute_value).ToArray();

                        //kiểm tra sản phẩm có chứa thuộc tính filter
                        bool isSubset = !lstFilter_size.Except(productAttributes).Any();

                        return isSubset;
                    }).ToList();
                }

                //Filter color 
                string filter_color = HttpContext.Current.Request?.Params["filter_color"];

                var lstFilter_color = String.IsNullOrEmpty(filter_color) ? new string[0] : filter_color.Split(',');

                if (lstFilter_color.Length > 0)
                {
                    lstFilter = lstFilter.Where(p =>
                    {
                        var productAttributes = db.getAttributeProduct(p.id).Select(x => x.attribute_value).ToArray();

                        bool isSubset = !lstFilter_color.Except(productAttributes).Any();

                        return isSubset;
                    }).ToList();
                }

                //Sort by condition

                string sort_by = HttpContext.Current.Request.Params["sort_by"];

                //if (sort_by.Length > 0)
                //{
                //    lstFilter = lstFilter.OrderBy(p => p.price).ToList();
                //}

                return lstFilter;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public product getProductById(int id)
        {
            return (product) db.getProductById(id).ToArray()[0];
        }

        public List<getAttributeProduct_Result> getAttributeByIdProduct(int id)
        {
            return db.getAttributeProduct(id).ToList();
        }

        public List<getProductWithAttribute_Result> getProductWithAttribute()
        {
            return db.getProductWithAttribute().ToList();
        }

        public List<product> getAllProduct()
        {
            return db.products.ToList();
        }

    }
}
