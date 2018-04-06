﻿using OnlineShoppingStore.Domain.Abstract;
using OnlineShoppingStore.Domain.Entities;
using OnlineShoppingStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderprocessor;
        public CartController(IProductRepository repo,IOrderProcessor proc)
        {
            orderprocessor = proc;
            repository = repo;
        }
        public ViewResult Index(Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = cart,ReturnUrl=returnUrl});
        }
        //public ViewResult Checkout()
        //{
        //    return View(new ShippingDetails());
        //}
        
        public ViewResult Checkout(Cart cart,ShippingDetails shippingDetails)
        {
            if (cart.lines.Count() == 0)
            {
                ModelState.AddModelError("", "sorry,your cart is empty!");
            }
            if (ModelState.IsValid)
            {

                orderprocessor.ProcessOrder(cart,shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
        public RedirectToRouteResult AddToCart(Cart cart,int productId,string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductId == productId);
            if(product!=null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index",new { returnUrl});
        }
        public RedirectToRouteResult RemoveFromCart(Cart cart,int productId,string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductId == productId);
            if(product!=null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index",new { returnUrl});
        }
        //private Cart GetCart()
        //{
        //    Cart cart = (Cart)Session["Cart"];
        //    if(cart==null)
        //    {
        //        cart = new Cart();
        //        Session["Cart"] = cart;
        //    }
        //    return cart;
        //}
    }
}