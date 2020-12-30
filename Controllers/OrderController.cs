using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telephone.Models;

namespace Telephone.Controllers
{
    public class OrderController : Controller
    {
        telephoneEntities DB = new telephoneEntities();


        public ActionResult Index(int ? PageNum)
        {
             
            return View(DB.CheckOut1.ToList().ToPagedList(PageNum ?? 1, 20));
         
        }

        
     
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return View("Index");
            }
            var order = DB.CheckOut1.Find(id);
            if (order==null)
            {
                return HttpNotFound();
            }
            return View(order);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {

            var pro = DB.CheckOut1.Where(e => e.ID_Order == id).FirstOrDefault();
            return View(pro);
        }
        [HttpPost]
        public ActionResult Delete(int id, CheckOut1 pro)
        {
           CheckOut1 product = DB.CheckOut1.Where(e => e.ID_Order == id).FirstOrDefault();
            OrderDetails order = DB.OrderDetails.Where(e => e.ID_Order == id).FirstOrDefault();
            order = DB.OrderDetails.Where(e => e.ProId == id).FirstOrDefault();
            DB.OrderDetails.Remove(order);
            DB.CheckOut1.Remove(product);
            DB.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}