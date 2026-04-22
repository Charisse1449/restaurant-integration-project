using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantPOS.Entities
{
    class MenuItem
    {
        private int menuItem_ID;
        private string name;
        private string type;
        private double price;
        private string quantity;
        private string description;

        public int MenuItem_ID
        {
            get { return menuItem_ID; }
            set { menuItem_ID = value; }
        }

        //public int MenuItem_ID { get => menuItem_ID; set => menuItem_ID = value; }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public string Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
       
    }
}
