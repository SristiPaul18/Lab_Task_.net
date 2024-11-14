using LAB_TASK.DTOs;
using LAB_TASK.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LAB_TASK.Controllers
{
    public class ProductController : Controller
    {
        LAB_TASKEntities db = new LAB_TASKEntities();

        private void PopulateCategories()
        {
            ViewBag.Categories = new List<string> { "Electronics", "Furniture", "Clothing", "Books", "Toys" };
        }

        public static Product Convert(ProductDTO d)
        {
            return new Product
            {
                ProductId = d.ProductId,
                Name = d.Name,
                Description = d.Description,
                Price = d.Price,
                StockQuantity = d.StockQuantity,
                Category = d.Category,
            };
        }

        public static ProductDTO Convert(Product d)
        {
            return new ProductDTO
            {
                ProductId = d.ProductId,
                Name = d.Name,
                Description = d.Description,
                Price = d.Price,
                StockQuantity = d.StockQuantity,
                Category = d.Category,
            };
        }

        public static List<ProductDTO> Convert(List<Product> data)
        {
            var list = new List<ProductDTO>();
            foreach (var d in data)
            {
                list.Add(Convert(d));
            }
            return list;
        }

        public ActionResult Index()
        {
            var data = db.Products.ToList();
            return View(Convert(data));
        }


        [HttpGet]
        public ActionResult Create()
        {
            PopulateCategories();
            return View(new ProductDTO());
        }

        [HttpPost]
        public ActionResult Create(ProductDTO d)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(Convert(d));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateCategories();
            return View(d);
        }

        public ActionResult Details(int id)
        {
            var exobj = db.Products.Find(id);
            return View(Convert(exobj));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var exobj = db.Products.Find(id);
            PopulateCategories();
            return View(Convert(exobj));
        }

        [HttpPost]
        public ActionResult Edit(ProductDTO d)
        {
            if (ModelState.IsValid)
            {
                var exobj = db.Products.Find(d.ProductId);
                db.Entry(exobj).CurrentValues.SetValues(d);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateCategories();
            return View(d);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var exobj = db.Products.Find(id);
            if (exobj == null)
            {
                TempData["ErrorMessage"] = "Product not found or may have already been deleted.";
                return RedirectToAction("List");
            }
            return View(Convert(exobj));
        }

        [HttpPost]
        public ActionResult Delete(int Id, string dcsn)
        {
            if (dcsn.Equals("Yes"))
            {
                var exobj = db.Products.Find(Id);
                db.Products.Remove(exobj);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
