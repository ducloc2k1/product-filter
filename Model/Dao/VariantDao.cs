using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public class VariantDao
    {
        private product_has_attributeEntities db = new product_has_attributeEntities();

        public variant GetVariantBySku(int idProduct, string sku)
        {
            return (variant) db.getVariantBySku(idProduct, sku).ToArray()[0];
        }
    }
}
