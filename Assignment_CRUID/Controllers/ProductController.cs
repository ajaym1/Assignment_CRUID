using Assignment_CRUID.Models;
using Assignment_CRUID.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_CRUID.Controllers
{
    public class ProductController : Controller
    {
        DbLayer _db;

        public ProductController(DbLayer db)
        {
            _db = db;
        }

        //GET Products
        public IActionResult Index()
        {
            var products = _db.ProductsList();
            return View(products);
        }
        //Create Products
        public IActionResult Create()
        {
            ViewBag.Categories = _db.CategoriesList().ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product model)
        {
            try
            {
                ModelState.Remove("ProductId");
                if (ModelState.IsValid)
                {
                    _db.SaveProduct(model);
                    return RedirectToAction("Index");
                }
                ViewBag.Categories = _db.CategoriesList().ToList();
                return View();
            }
            catch
            {
                return View();
            }

        }
        //Create Products
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = _db.CategoriesList().ToList();
            var product = _db.GetProductById(id);
            return View("Create", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.UpdateProduct(model);
                    return RedirectToAction("Index");
                }
                ViewBag.Categories = _db.CategoriesList().ToList();
                return View("Create", model);
            }
            catch
            {
                return View("Create", model);
            }
        }

        public IActionResult Delete(int id)
        {
            Product model = _db.GetProductById(id);
            if (model != null)
            {
                _db.DeleteProduct(id);
            }
            return RedirectToAction("Index");
        }


    }
}
