using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_sklep
{
    public class Product
    {
        public int id {  get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }

        public Product(int id, string name, double price, int quantity)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.quantity = quantity;
        }
    }
}
