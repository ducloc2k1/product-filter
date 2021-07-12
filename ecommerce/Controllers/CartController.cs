using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ecommerce.Models;
using Model.Dao;
using Model.EF;

namespace ecommerce.Controllers
{


    public class CartController : Controller
    {
        private string CartSession = "CartSession";

        private product_has_attributeEntities db = new product_has_attributeEntities();

        private ProductDao productDao = new ProductDao();

        // GET: Cart
        public string Index()
        {
            return "Đây là trang giỏ hàng";
        }

        public ActionResult AddItem(int idProduct, int quantity, string color="", string size="")
        {
            string skuProduct = $"{idProduct}{size}{color}";

            var cart = Session[CartSession];

            var product = productDao.getProductById(idProduct);

            if (cart != null)
            {
                //lấy list cart item từ Session
                var list = (List<CartItem>)cart;

                //kiểm tra sản phẩm có tồn trong list
                if(list.Exists(x => (x.product.id+x.size+x.color).Equals(skuProduct)))
                {
                    foreach (var cartItem in list)
                    {
                        string skuCartItem = $"{cartItem.product.id}{cartItem.size}{cartItem.color}";

                        if (skuProduct.Equals(skuCartItem)){
                            cartItem.quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới cart item
                    var cartItem = new CartItem();
                    cartItem.product = product;
                    cartItem.quantity = quantity;
                    cartItem.color = color;
                    cartItem.size = size;

                    list.Add(cartItem);
                }
            }
            else
            {
                //tạo mới đối tượng carItem
                var cartItem = new CartItem();
                cartItem.product = product;
                cartItem.quantity = quantity;
                cartItem.color = color;
                cartItem.size = size;

                //tạo mới list
                var list = new List<CartItem>();
                list.Add(cartItem);

                //gán vào Session
                Session[CartSession] = list;
            }
            
            return RedirectToAction("Index");
        }

    }
}