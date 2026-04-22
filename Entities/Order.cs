using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantPOS.Entities
{
    class Order
    {
        private int order_ID;
        private int table_ID;
        private char status;
        

        public int Order_ID
        {
            get { return order_ID; }
            set { order_ID = value; }
        }

        public int Table_ID
        {
            get { return table_ID; }
            set { table_ID = value; }
        }

        public char Status
        {
            get { return status; }
            set { status = value; }
        }

        
    }
}
