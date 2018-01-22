using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repositoryService)
        {
            this.repository = repositoryService;
        }

        public IActionResult Index()
        {
            return View(repository.Products);
        }

        public IActionResult Edit(int productID)
        {
            SelectList listCategory = new SelectList(repository.Products.Select(s => s.Category).Distinct().AsQueryable().AsNoTracking().ToList());
            ViewBag.listCategory = listCategory;

            return View(repository.Products.FirstOrDefault(p => p.ProductID == productID));
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);

                //В нем хранится инфа пока не будет прочитана
                //Отличается от ViewBag тем, что там инфа хранится только на момент запроса http для передачи от контроллера к представлению
                TempData["message"] = $"{product.Name} успешно сохранен";

                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public IActionResult Create()
        {
            SelectList listCategory = new SelectList(repository.Products.Select(s => s.Category).Distinct().AsQueryable().AsNoTracking().ToList());
            ViewBag.listCategory = listCategory;

            return  View("Edit", new Product());
        }

        [HttpPost]
        public IActionResult Delete(int productID)
        {
            Product product = repository.DeleteProduct(productID);
            if (product != null)
            {
                TempData["message"] = $"{product.Name} успешно удален";
            }
            return RedirectToAction("Index");
        }
    }
}