﻿using OnlineShoppingStore.Domain.Abstract;
using OnlineShoppingStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository repository;
        public int PageSize = 3;
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }
        public ViewResult List( string category,int page=1)
        {
            ProductListViewModel model = new ProductListViewModel
            {
                Products = repository.Products
                                 .Where(p => category == null || p.category == category)
                                 .OrderBy(p => p.ProductId)
                                 .Skip((page - 1) * PageSize)
                                 .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems =    category==null ?
                                    repository.Products.Count() :
                                    repository.Products.Where(p=>p.category == category).Count()
                },
                CurrentCategory = category
                                 
            };
            return View(model);
        }
    }
}