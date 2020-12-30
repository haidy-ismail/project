using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telephone.Models;
using System.Data.Entity;
using System.Data;
using System.Net;
using PagedList.Mvc;
using PagedList;
namespace Telephone.Controllers
{
    public class ProductController : Controller
    {

        private telephoneEntities db = new telephoneEntities();

        // GET: Product
        [HttpGet]
        public ActionResult Index1(int? PageNum )
        {
                 return View(db.Product.ToList().ToPagedList(PageNum ?? 1, 20));
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                Product phone = db.Product.Where(p => p.ProId== id).FirstOrDefault();

                if (phone != null)
                {
                    return View(phone);
                }
            }
            return RedirectToAction("Index1");
        }
        [HttpPost]
        public ActionResult Index1(string option ,string search,int? PageNum)
        {
            if (option == "Name")
            {
                return View(db.Product.Where(x => x.model.Contains(search) || search == null).ToList().ToPagedList(PageNum ?? 1, 20));
            }
            else if (option=="Brand") 
            {
                return View(db.Product.Where(x => x.brand == search || search == null).ToList().ToPagedList(PageNum ?? 1, 20));
            }
            return RedirectToAction("Index1");
        }
       
       
    }
}
