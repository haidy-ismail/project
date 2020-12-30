using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Telephone.Models;

namespace Telephone.Controllers
{
    public class CompanyController : Controller
    {


        telephoneEntities DB = new telephoneEntities();


        [HttpGet]
        public ActionResult Index(int? PageNum)
        {
            return View(DB.ProductOfCompany.ToList().ToPagedList(PageNum ?? 1, 20));
        }
        [HttpPost]
        public ActionResult Index(string option, string search, int? PageNum)
        {
            if (option == "Name")
            {
                return View(DB.ProductOfCompany.Where(x => x.model.Contains(search) || search == null).ToList().ToPagedList(PageNum ?? 1, 20));
            }
            else if (option == "Brand")
            {
                return View(DB.ProductOfCompany.Where(x => x.brand == search || search == null).ToList().ToPagedList(PageNum ?? 1, 20));
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Register()
        {
            Company com = new Company();
            return View(com);
        }
        [HttpPost]
        public ActionResult Register(Company company)
        {
            Company com = new Company();

            com.Id_Company = company.Id_Company;
            com.Fname = company.Fname;
            com.Lname = company.Lname;
            com.Email = company.Email;
            com.Password = company.Password;
            com.Phone = company.Phone;

            if (ModelState.IsValid) {
                DB.Company.Add(com);
                DB.SaveChanges();

                return RedirectToAction("Profile");
            }
            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Company company)
        {
            var data = DB.Company.Where(s => s.Email == company.Email && s.Password == company.Password).FirstOrDefault();
            if (data != null )
            
            {
                Session["Login"]=company ;
                return RedirectToAction("Profile");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        public ActionResult LogOut()
        {
            Session.Clear();
            return View("Index1", "Product");
        }

        [HttpGet]
        public ActionResult Profile(int? PageNum)
        {


            if (Session["Login"] == null)
            {
                return RedirectToAction("Login");
            }
            return View(DB.ProductOfCompany.ToList().ToPagedList(PageNum ?? 1, 20));
        }




        [HttpGet]
        public ActionResult Create()
        {

            ProductOfCompany pro = new ProductOfCompany();
            return View(pro);
        }
        [HttpPost]
        public ActionResult Create(ProductOfCompany product)
        {

            string filename = Path.GetFileNameWithoutExtension(product.imagefile.FileName);
            string extetion = Path.GetExtension(product.imagefile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extetion;
            product.img = "~/MyImges/images/" + filename;
            filename = Path.Combine(Server.MapPath("~/MyImges/images/"), filename);
           
                product.imagefile.SaveAs(filename);
            if (ModelState.IsValid)
            {
                DB.ProductOfCompany.Add(product);
                DB.SaveChanges();

                return View("Index");
            }
            return View();
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {

            var pro = DB.ProductOfCompany.Where(e => e.Id_Of_Product == id).FirstOrDefault();
            return View(pro);
        }
        [HttpPost]
        public ActionResult Delete(int id, ProductOfCompany pro)
        {
            ProductOfCompany product = DB.ProductOfCompany.Where(e => e.Id_Of_Product == id).FirstOrDefault();



            DB.ProductOfCompany.Remove(product);
            DB.SaveChanges();
            return RedirectToAction("Profile");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {

            var pro = DB.ProductOfCompany.Where(e => e.Id_Of_Product == id).FirstOrDefault();
            return View(pro);
        }
        [HttpPost]
        public ActionResult Edit(int id, ProductOfCompany pro)
        {
            ProductOfCompany product = new ProductOfCompany();
            product = DB.ProductOfCompany.Where(e => e.Id_Of_Product == id).FirstOrDefault();

            product.brand = pro.brand;
            product.approx_price_EGP = pro.approx_price_EGP;
            product.availabality = pro.availabality;
            product.battery = pro.battery;
            product.colors = pro.colors;
            product.CPU = pro.CPU;
            product.display_resolution = pro.display_resolution;
            product.display_type = pro.display_type;
            product.GPRS = pro.GPRS;
            product.GPU = pro.GPU;
            product.internal_memory = pro.internal_memory;
            product.memory_card = pro.memory_card;
            product.model = pro.model;
            product.OS = pro.OS;
            product.primary_camera = pro.primary_camera;
            product.RAM = pro.RAM;
            product.secondary_camera = pro.secondary_camera;
            product.sensors = pro.sensors;
            product.SIM = pro.SIM;
            product.weight_g = pro.weight_g;
           
                DB.Entry(product).State = EntityState.Modified;
                DB.SaveChanges();

                return RedirectToAction("Profile");
            
           
        }




       


    }
}