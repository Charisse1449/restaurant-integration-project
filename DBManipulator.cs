using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS
{
    class DBManipulator
    {
        public DBManipulator()
        {
            // Disabled. System now uses Laravel API only.
        }

        public SqlConnection GetConnection()
        {
            throw new InvalidOperationException("Direct SQL is disabled. Use Laravel API instead.");
        }

        public SqlCommand GetCommand()
        {
            throw new InvalidOperationException("Direct SQL is disabled. Use Laravel API instead.");
        }
    }
}

