using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerceSite.DbConfig;
using ECommerceSite.LogicLayer;
using ECommerceSite.Models;
using ECommerceSite.ECommerceGPPDOperations;
using Newtonsoft.Json;

namespace ECommerceSite.Controllers
{
    //maaz first change
    // fghfgh
    //change by Osyed
    public class ProductsController : Controller
    {
        // first change from muzammil.
        //hafsa first change
        //lkj
        //ItxNoob
        //first change hafsa





        //first change Moiza



        //first change Moiza

        //first change Moiza


        // silly mistakes
        private ECommerceSiteEntities db = new ECommerceSiteEntities();
        GPPD_ServiceSoapClient soap = new GPPD_ServiceSoapClient();

        //first change tanzeela

        //vvvvvv   //sdfsdf
        #region AdminArea

        #region List

        // GET: Products
        public ActionResult Index()
        {
            if (Authentication.GetSessionDetail() == null)
            {
                return RedirectToAction("Login", "Autheticator");
            }

            var productJson = soap.GetAllProducts();

            var products = JsonConvert.DeserializeObject<List<Product>>(productJson);

            // db.Products.Where(x => !x.Deleted).ToList();

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
        //sdfsdf
        #region Create

        // GET: Products/Create
        public ActionResult Create()
        {
            if (Authentication.GetSessionDetail() == null)
            {
                return RedirectToAction("Login", "Autheticator");
            }
            var model = new ProductModel();
            PrepareCategoryDropdown(model);

            return View(model);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductModel model, HttpPostedFileBase file)
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
                product.CategoryId = model.CategoryId;
                product.CreatedOnUtc = DateTime.UtcNow;
                product.CreatedBy = Convert.ToInt32(Session["UserId"]);
                product.ImagePath = "";

                var productJson = JsonConvert.SerializeObject(product);

                soap.InsertProduct(productJson);

                //db.Products.Add(product);
                //db.SaveChanges();

                //var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                //string myfile = product.Id + ext; //appending the name with id  
                //var path = Path.Combine(Server.MapPath("~/WebContent/Products"), myfile);
                //file.SaveAs(path);

                //product.ImagePath = path;
                //db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        #endregion
        //sdfsdf
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
                return RedirectToAction("Index");
            }

            var category = db.Categories.Where(x => !x.Deleted && x.Id == product.CategoryId).FirstOrDefault();

            var model = new ProductModel();
            model.Id = product.Id;
            model.Name = product.Name;
            model.Description = product.Description;
            model.CategoryId = product.CategoryId;
            if (category != null)
            {
                model.CategoryName = category.Name;
            }
            model.SupplierId = product.SupplierId;
            model.SalePrice = product.SalePrice;
            model.CostPrice = product.CostPrice;
            model.CreatedBy = product.CreatedBy;
            model.CreatedOnUtc = product.CreatedOnUtc;
            model.UpdatedBy = product.UpdatedBy;
            model.Deleted = product.Deleted;
            model.OutOfStock = product.OutOfStock;

            PrepareCategoryDropdown(model);

            return View(model);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductModel model)
        {
            if (Authentication.GetSessionDetail() == null)
            {
                return RedirectToAction("Login", "Autheticator");
            }
            if (ModelState.IsValid)
            {
                var product = db.Products.Where(x => !x.Deleted && x.Id == model.Id).FirstOrDefault();
                if (product == null)
                {
                    return RedirectToAction("Index");
                }
                product.Name = model.Name;
                product.Description = model.Description;
                product.CostPrice = model.CostPrice;
                product.SalePrice = model.SalePrice;
                product.OutOfStock = model.OutOfStock;
                product.CategoryId = model.CategoryId;
                product.UpdatedOnUtc = DateTime.UtcNow;
                product.UpdatedBy = Convert.ToInt32(Session["UserId"]);

                var productJson = JsonConvert.SerializeObject(product);
                soap.UpdateProduct(productJson);


                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public void PrepareCategoryDropdown(ProductModel model)
        {
            var categories = db.Categories.Where(x => !x.Deleted).ToList();
            model.CategoriesDropDown.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            foreach (var categpry in categories)
            {
                model.CategoriesDropDown.Add(new SelectListItem { Text = categpry.Name, Value = categpry.Id.ToString(), Selected = model.CategoryId == categpry.Id });
            }
        }

        #endregion
        //sdfsdf
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
            soap.DeleteProduct(id.Value);

            return RedirectToAction("Index");
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
       // my changes
        #endregion
        //sdfsdf
        #endregion
        //sdfsdf
        #region CustomerArea
        //sdfsfdsf
        public ActionResult Menu()
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
                    productModel.ImagePath = "/WebContent/Products/" + product.Id + Path.GetExtension(product.ImagePath);

                    productlist.Add(productModel);
                }
            }

            return View(productlist);
        }
        //sdfsdf
        [HttpPost]
        public JsonResult AddToCart(int productId, int qty)
        {
            var userId = Convert.ToInt32(Session["UserId"]);

            if (Authentication.GetSessionDetail() == null)
            {
                return Json("please loging first", JsonRequestBehavior.AllowGet);
            }

            if (productId > 0 && qty > 0)
            {
                var shopcart = db.ShoppingCarts.Where(x => x.ProductId == productId && x.UserId == userId).FirstOrDefault();
                if (shopcart != null)
                {
                    shopcart.Qauntity = shopcart.Qauntity + qty;
                    shopcart.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                    shopcart.UpdatedOnUtc = DateTime.UtcNow;
                    db.SaveChanges();
                }
                else
                {
                    ShoppingCart shoppingCart = new ShoppingCart();
                    shoppingCart.ProductId = productId;
                    shoppingCart.Qauntity = qty;
                    shoppingCart.UserId = Convert.ToInt32(Session["UserId"]);
                    shoppingCart.CreatedOnUtc = DateTime.UtcNow;
                    db.ShoppingCarts.Add(shoppingCart);
                    db.SaveChanges();
                }

                var allItems = db.ShoppingCarts.Where(x => x.UserId == userId).ToList();
                if (allItems != null)
                {
                    return Json(allItems.Count.ToString(), JsonRequestBehavior.AllowGet);
                }
                return Json("0", JsonRequestBehavior.AllowGet);

            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }
        //sdfsdf
        //sdfsdf
        public ActionResult Cart()
        {
            if (Authentication.GetSessionDetail() == null)
            {
                return RedirectToAction("Login", "Autheticator");
            }

            return View();
        }
        //sdfsdf
        #endregion
        //First change Arham
    }
}


// CTRL + M + O