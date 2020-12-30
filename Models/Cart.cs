using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telephone.Models
{
    public class Cart
    {
        public Product product { get; set; }
        public int quantity { get; set; }
        public Cart(Product Product,int Quantity)
        {
            product = Product;
            quantity = Quantity;

        }
    }
}