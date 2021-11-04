using BussinessLayer.Concrete;
using BussinessLayer.ValidationRules;
using CoreDemo.Models;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class RegisterController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterRepository());
        [HttpGet]
        public IActionResult Index()
        {
            var cities = new List<CityModel>() {
            new CityModel(){CityId=1,CityName="İstanbul" },
            new CityModel(){CityId=2,CityName="İzmir" },
            new CityModel(){CityId=3,CityName="Çanakkale" },
            new CityModel(){CityId=4,CityName="Sakarya" },
            new CityModel(){CityId=5,CityName="Ankara" }
            };
            ViewBag.cities = new SelectList(cities,"CityId","CityName");
            return View();
        }
        [HttpPost]
        public IActionResult Index(Writer p)
        {
            WriterValidator wv = new WriterValidator();
            ValidationResult results = wv.Validate(p);
            if (results.IsValid)
            {
                p.WriterStatus = true;
                p.WriterAbout = "Deneme Test";
                wm.TAdd(p);
                return RedirectToAction("Index", "Blog");
            }
            else
            {
                foreach(var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
           
        }
    }
}
