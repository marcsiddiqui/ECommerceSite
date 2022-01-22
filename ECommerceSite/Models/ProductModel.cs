using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceSite.Models
{
    public class ProductModel
    {
        public ProductModel()
        {
            CategoriesDropDown = new List<SelectListItem>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
        public bool Deleted { get; set; }
        public bool OutOfStock { get; set; }
        public List<SelectListItem> CategoriesDropDown { get; set; }
    }
}