using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telephone.Models;

namespace Telephone.Controllers
{
    public class ShoppingCartController : Controller
    {
        telephoneEntities DB = new telephoneEntities();
        private string srtcart = "Cart";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderNow(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index1", "Product");

            }
            if (Session[srtcart] == null)
            {
                List<Cart> lscart = new List<Cart>
                {
                    new Cart(DB.Product.Find(id),1)
            };
                Session[srtcart] = lscart;
            }
            else
            {

                List<Cart> lscart = (List<Cart>)Session[srtcart];
                int check = isExistingCheck(id);
                if (check == -1)
                    lscart.Add(new Cart(DB.Product.Find(id), 1));

                else
                    lscart[check].quantity++;

                Session[srtcart] = lscart;
            }
            return View("Index");
        }

        private int isExistingCheck(int? id)

        {

            List<Cart> lscart = (List<Cart>)Session[srtcart];
            for (int i = 0; i < lscart.Count; i++)
            {


                if (lscart[i].product.ProId == id)
                    return i;
            }
            return -1;

        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index1", "Product");
            }

            int check = isExistingCheck(id);
            List<Cart> lscart = (List<Cart>)Session[srtcart];
            lscart.RemoveAt(check);
            return View("Index");

        }



        [HttpGet]
        public ActionResult UpdateCart()
        {
            
            return View();

        }


        [HttpPost]
        public ActionResult UpdateCart(FormCollection frc)
        {

            string[] quantities = frc.GetValues("quantity");

            List<Cart> lstCart = (List<Cart>)Session[srtcart];
            for (int i = 0; i < lstCart.Count; i++)
            {
                lstCart[i].quantity = Convert.ToInt32(quantities[i]);


            }
            Session[srtcart] = lstCart;
            return View("Index");

        }


        [HttpGet]
        public ActionResult Buying()
        {
            CheckOut1 order = new CheckOut1();
            return View(order);
        }

        [HttpPost]
        public ActionResult Buying(FormCollection frc)
        {
            CheckOut1 order = new CheckOut1()
            {
                OrderName = frc["OrderName"],
                Fname = frc["Fname"],
                Lname = frc["Lname"],
                Phone = frc["Phone"],
                Email = frc["Email"],
                Address=frc["Address"],
                City=frc["City"],
                Credit_Card=frc["Credit_Card"],
              
            
                OrderDate = DateTime.Now,
               PaymentType = "Cash",
              Status = "processing "

        };
            if (ModelState.IsValid)
            {
                DB.CheckOut1.Add(order);
                DB.SaveChanges();

            }
            List<Cart> lstCart = (List<Cart>)Session[srtcart];
            foreach (var cart in lstCart)

            {
                OrderDetails orderDetails = new OrderDetails()
                {
                    ID_Order=order.ID_Order,
                    ProId=cart.product.ProId,
                    Quantity=cart.quantity,
                    Price=int.Parse(cart.product.approx_price_EGP)

                };

                DB.OrderDetails.Add(orderDetails);
                DB.SaveChanges();
            }


            Session.Remove(srtcart);
            return View("OrderSuccess");
        }



        [HttpGet]
        public ActionResult OrderSuccess()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Confirmatoin( )
        {
            CheckOut1 order = new CheckOut1();
            return View(order);
        }



    }

}