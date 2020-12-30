using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telephone.Models;

namespace Telephone.Controllers
{
    public class CustomerController : Controller
    {
        telephoneEntities DB = new telephoneEntities();

        [HttpGet]
        public ActionResult Register()
        {
            Customer c = new Customer();
            return View(c);
        }
        [HttpPost]
        public ActionResult Register(Customer customer)
        {
            //Customer C = new Customer();
            //C.Id_Customer = customer.Id_Customer;
            //C.Fname = customer.Fname;
            //C.Lname = customer.Lname;
            //C.Email = customer.Email;
            //C.Password = customer.Password;
            //C.Phone = customer.Phone;
            //C.Credit_crad = customer.Credit_crad;
            //C.Address = customer.Address;
            Session["user"] = customer;
            if (ModelState.IsValid)
            {
                DB.Customer.Add(customer);
            DB.SaveChanges();

                return RedirectToAction("Index1", "Product");
            }
            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Customer customer)
        {
            var data = DB.Company.Where(s => s.Email == customer.Email && s.Password == customer.Password).FirstOrDefault();
            if (data != null && ModelState.IsValid)
            { 
                    
                Session["user"] =customer;
                return RedirectToAction("Index1","Product");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout() {

            Session.Clear();
            return RedirectToAction("Index1", "Product");
        }


       

    }
}