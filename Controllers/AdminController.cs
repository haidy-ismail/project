using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Telephone.Models;

namespace Telephone.Controllers
{
    public class AdminController : Controller
    {
        telephoneEntities DB = new telephoneEntities();

        [HttpGet]
        public ActionResult Profile(int? PageNum)
        {
            return View(DB.Product.ToList().ToPagedList(PageNum ?? 1, 20));
        }
        [HttpPost]
        public ActionResult Profile(string option, string search, int? PageNum)
        {
            if (option == "Name")
            {
                return View(DB.Product.Where(x => x.model.Contains(search) || search == null).ToList().ToPagedList(PageNum ?? 1, 20));
            }
            else if (option == "Brand")
            {
                return View(DB.Product.Where(x => x.brand == search || search == null).ToList().ToPagedList(PageNum ?? 1, 20));
            }
            return RedirectToAction("Index1");
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Adminstrator adminstrator)
        {
            var data = DB.Adminstrator.Where(s => s.Email == adminstrator.Email && s.Password == adminstrator.Password).FirstOrDefault();
            if (ModelState.IsValid && data!=null)
            {
                
                    Session["Login"] = adminstrator;
                    return RedirectToAction("Profile");
                
            }
            else
            {

                return RedirectToAction("Index1");
            }
            
        }

        public ActionResult Logout()
        {

            Session.Clear();
            return RedirectToAction("Index1", "Product");
        }


        [HttpGet]
        public ActionResult AddProduct()
        {
            Product pro = new Product();
            return View(pro);
        }
        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            string filename = Path.GetFileNameWithoutExtension(product.imagefile.FileName);
            string extetion = Path.GetExtension(product.imagefile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extetion;
            product.img = "~/MyImges/images/" + filename;
            filename = Path.Combine(Server.MapPath("~/MyImges/images/"), filename);
            if (ModelState.IsValid)
            {
                product.imagefile.SaveAs(filename);
                DB.Product.Add(product);
                DB.SaveChanges();

                return View();
            }
            return View();
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {

            var pro = DB.Product.Where(e => e.ProId == id).FirstOrDefault();
            return View(pro);
        }
        [HttpPost]
        public ActionResult Delete(int id, Product pro)
        {
            Product product = DB.Product.Where(e => e.ProId == id).FirstOrDefault();



            DB.Product.Remove(product);
            DB.SaveChanges();
            return RedirectToAction("Profile");
        }



        [HttpGet]
        public ActionResult Edit(int id)
        {

            var pro = DB.Product.Where(e => e.ProId == id).FirstOrDefault();
            return View(pro);
        }
        [HttpPost]
        public ActionResult Edit(int id, Product pro)
        {
            //Product product = new Product();
            //product = DB.Product.Where(e => e.ProId == id).FirstOrDefault();

          
            //product.brand = pro.brand;
            //product.approx_price_EGP = pro.approx_price_EGP;
            //product.availabality = pro.availabality;
            //product.battery = pro.battery;
            //product.colors = pro.colors;
            //product.CPU = pro.CPU;
            //product.display_resolution = pro.display_resolution;
            //product.display_type = pro.display_type;
            //product.GPRS = pro.GPRS;
            //product.GPU = pro.GPU;
            //product.internal_memory = pro.internal_memory;
            //product.memory_card = pro.memory_card;
            //product.model = pro.model;
            //product.OS = pro.OS;
            //product.primary_camera = pro.primary_camera;
            //product.RAM = pro.RAM;
            //product.secondary_camera = pro.secondary_camera;
            //product.sensors = pro.sensors;
            //product.SIM = pro.SIM;
            //product.weight_g = pro.weight_g;
  
                DB.Entry(pro).State = EntityState.Modified;
                DB.SaveChanges();

                return RedirectToAction("Profile");
            
            
        }








      

       



    }
}