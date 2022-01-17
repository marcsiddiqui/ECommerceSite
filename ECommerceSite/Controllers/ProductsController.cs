using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerceSite.DbConfig;
using ECommerceSite.LogicLayer;
using ECommerceSite.Models;

namespace ECommerceSite.Controllers
{
    public class ProductsController : Controller
    {
        private ECommerceSiteEntities db = new ECommerceSiteEntities();

        #region AdminArea

        #region List

        // GET: Products
        public ActionResult Index()
        {
            if (Authentication.GetSessionDetail() == null)
            {
                return RedirectToAction("Login", "Autheticator");
            }

            var products = db.Products.Where(x => !x.Deleted).ToList();
            var categories = db.Categories.Where(x => !x.Deleted).ToList();

            var productlist = new List<ProductModel>();

            if (products != null && products.Any())
            {
                foreach (var product in products)
                {
                    var productModel = new ProductModel();

                    productModel.Id = product.Id;
                    productModel.Name = product.Name;
                    productModel.Description = product.Description;
                    productModel.CategoryId = product.CategoryId;
                    if (product.CategoryId > 0)
                    {
                        if (categories != null && categories.Any())
                        {
                            var category = categories.Where(x => x.Id == product.CategoryId).FirstOrDefault();
                            if (category != null)
                            {
                                productModel.CategoryName = category.Name;
                            }
                        }
                    }
                    productModel.SupplierId = product.SupplierId;
                    productModel.SalePrice = product.SalePrice;
                    productModel.CostPrice = product.CostPrice;
                    productModel.CreatedBy = product.CreatedBy;
                    productModel.CreatedOnUtc = product.CreatedOnUtc;
                    productModel.UpdatedBy = product.UpdatedBy;
                    productModel.UpdatedOnUtc = product.UpdatedOnUtc;
                    productModel.SupplierName = "Mushtaq";
                    productModel.Deleted = product.Deleted;
                    productModel.OutOfStock = product.OutOfStock;

                    productlist.Add(productModel);
                }
            }

            return View(productlist);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (Authentication.GetSessionDetail() == null)
            {
                return RedirectToAction("Login", "Autheticator");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        #endregion

        #region Create

        // GET: Products/Create
        public ActionResult Create()
        {
            if (Authentication.GetSessionDetail() == null)
            {
                return RedirectToAction("Login", "Autheticator");
            }
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductModel model)
        {
            if (Authentication.GetSessionDetail() == null)
            {
                return RedirectToAction("Login", "Autheticator");
            }
            if (ModelState.IsValid)
            {
                Product product = new Product();
                product.Name = model.Name;
                product.Description = model.Description;
                product.CostPrice = model.CostPrice;
                product.SalePrice = model.SalePrice;
                product.OutOfStock = model.OutOfStock;

                product.CreatedOnUtc = DateTime.UtcNow;
                product.CreatedBy = 1;

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        #endregion

        #region Eidt

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Authentication.GetSessionDetail() == null)
            {
                return RedirectToAction("Login", "Autheticator");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
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
        public ActionResult Edit([Bind(Include = "Id,Name,Description,CategoryId,SupplierId,SalePrice,CostPrice,CreatedBy,CreatedOnUtc,UpdatedBy,UpdatedOnUtc,Deleted,OutOfStock")] Product product)
        {
            if (Authentication.GetSessionDetail() == null)
            {
                return RedirectToAction("Login", "Autheticator");
            }
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        #endregion

        #region Delete

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Authentication.GetSessionDetail() == null)
            {
                return RedirectToAction("Login", "Autheticator");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
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
            if (Authentication.GetSessionDetail() == null)
            {
                return RedirectToAction("Login", "Autheticator");
            }
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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

        #endregion

        #endregion

        #region CustomerArea

        public ActionResult Menu()
        {
            var products = db.Products.Where(x => !x.Deleted).ToList();
            var categories = db.Categories.Where(x => !x.Deleted).ToList();

            var productlist = new List<ProductModel>();

            if (products != null && products.Any())
            {
                foreach (var product in products)
                {
                    var productModel = new ProductModel();

                    productModel.Id = product.Id;
                    productModel.Name = product.Name;
                    productModel.Description = product.Description;
                    productModel.CategoryId = product.CategoryId;
                    if (product.CategoryId > 0)
                    {
                        if (categories != null && categories.Any())
                        {
                            var category = categories.Where(x => x.Id == product.CategoryId).FirstOrDefault();
                            if (category != null)
                            {
                                productModel.CategoryName = category.Name;
                            }
                        }
                    }
                    productModel.SupplierId = product.SupplierId;
                    productModel.SalePrice = product.SalePrice;
                    productModel.CostPrice = product.CostPrice;
                    productModel.CreatedBy = product.CreatedBy;
                    productModel.CreatedOnUtc = product.CreatedOnUtc;
                    productModel.UpdatedBy = product.UpdatedBy;
                    productModel.UpdatedOnUtc = product.UpdatedOnUtc;
                    productModel.SupplierName = "Mushtaq";
                    productModel.Deleted = product.Deleted;
                    productModel.OutOfStock = product.OutOfStock;

                    productlist.Add(productModel);
                }
            }

            return View(productlist);
        }

        #endregion
    }
}


// CTRL + M + O