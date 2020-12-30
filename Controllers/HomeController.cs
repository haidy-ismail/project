using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telephone.Models;
using System.Data.Entity;
using System.Data;
using System.Net;


namespace Telephone.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("Index1", "Product");
        }
    }
}