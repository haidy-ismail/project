using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telephone.Models;

namespace Telephone.Controllers
{
    public class ServicesController : Controller
    {
        telephoneEntities DB = new telephoneEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Services services)
        {
            var data = DB.Services.Where(s => s.Email == services.Email && s.Password == services.Password).FirstOrDefault();
            if ( data != null)
            {

                Session["Login"] = services;
                return RedirectToAction("Index");

            }
            else
            {

                return RedirectToAction("Index1","Product");
            }

        }
        public ActionResult Logout()
        {

            Session.Clear();
            return RedirectToAction("Index1", "Product");
        }

    }
}