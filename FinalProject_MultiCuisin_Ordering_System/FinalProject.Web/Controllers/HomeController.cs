﻿using FinalProject.Service.Interfaces;
using FinalProject.Service.Services;
using FinalProject.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using FinalProject.Data.Repository;
namespace FinalProject.Web.Controllers
{
    public class HomeController : Controller
    {
       readonly IProductService _ProductService=null;
        readonly ICategoryService _CategoryService;
       public HomeController(IProductService productService,ICategoryService categoryService)
       {
           _CategoryService = categoryService;
           _ProductService = productService;
       }
        
        public ActionResult Index(string category)
        {
            
            IList<Domain.Model.Product> p;
            ProductDalRepository productDal = new ProductDalRepository();
            ProductService productService = new ProductService(productDal);
            p = productService.GetProducts();
            if(!(String.IsNullOrEmpty(category)))
            {
                return View(productService.GetProducts().ToList().Where(c => c.Title == category));
            }
            return View(p);
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, Product product)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    //IProductRepository dal = new ProductDalRepository();
                    //IProductService productService = new ProductService();
                    string path = Path.Combine(Server.MapPath("~/Images"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    product.ImageUrl = path;      
                   
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }

            return View();
        }

        public ActionResult About()
        {            
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       
    }
}