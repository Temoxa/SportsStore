using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;

        private int PageSize = 4;

        public ProductController(IProductRepository serviceRepository)
        {
            this.repository = serviceRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            return View(new ProductsListViewModel {
                Products = repository.Products
                        .AsQueryable()
                        .AsNoTracking()
                        .Where(p => category == null || p.Category == category)
                        .OrderBy(p => p.ProductID)
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize)
                        .ToList(),
                PagingInfo = new PagingInfo { CurrentPage = page,
                                ItemsPerPage = PageSize,
                                TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(p => p.Category == category).Count()
                },
                CurrentCategory = category
            });
        }
    }
}