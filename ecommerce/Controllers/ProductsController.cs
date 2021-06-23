using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ecommerce.Models;

namespace ecommerce.Controllers
{
    public class ProductsController : Controller
    {
        private product_has_attributeEntities db = new product_has_attributeEntities();

        // GET: Products
        public ActionResult Index()
        {
            IEnumerable<getProductWithAttribute_Result1> filterList = null;

            try
            {
                var productWithAttr = db.getProductWithAttribute().ToList();

                string filter_size = Request.Params["filter_size"];

                //Filter size
                var lstFilter_size = filter_size.Split(',');


                if (lstFilter_size.Length > 0)
                {

                    filterList = productWithAttr
                        .Where(p =>
                        {
                            foreach (var size in lstFilter_size)
                            {
                                if (p.attribute_name.CompareTo("size") != 0 || size.CompareTo(p.attribute_value) != 0) return false;
                            }
                            return true; 
                        });
                }

                //Filter color
                string filter_color = Request.Params["filter_color"];

                var lstFilter_color = filter_color.Split(',');

                if (lstFilter_color.Length > 0)
                {
                    if (filterList != null)
                    {
                        filterList = filterList
                            .Where(p =>
                            {
                                foreach (var color in lstFilter_color)
                                {
                                    if (p.attribute_name.CompareTo("color") != 0 || color.CompareTo(p.attribute_value) != 0) return false;
                                }
                                return true;
                            });
                    }
                    else
                    {
                        filterList = productWithAttr
                            .Where(p =>
                            {
                                foreach (var size in lstFilter_size)
                                {
                                    if (p.attribute_name.CompareTo("color") != 0 || size.CompareTo(p.attribute_value) != 0) return false;
                                }
                                return true;
                            });
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }

            return View("");
        }


        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,price")] product product)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,price")] product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
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
