using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestaurantPOS.Entities;

namespace RestaurantPOS
{
    class Configurator
    {
        private DBManipulator manipulator;

        public Configurator()
        {
            this.manipulator = new DBManipulator();
        }

        /// <summary>
        /// Loads all tables in the Tables form.
        /// </summary>
        /// <returns></returns>
        public DataTable LoadTables()
        {
            DataTable result = new DataTable();

            result.Columns.Add("Table_ID");

            SqlConnection connection = this.manipulator.GetConnection();

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "SELECT Table_ID FROM dbo.[Table] ORDER BY Table_ID ASC";

                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        int table_ID = Convert.ToInt32(reader["Table_ID"]);

                        DataRow row = result.NewRow();

                        row[0] = table_ID;

                        result.Rows.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public DataTable LoadActiveTables()
        {
            DataTable result = new DataTable();

            result.Columns.Add("Table_ID");

            SqlConnection connection = this.manipulator.GetConnection();

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "select t.[Table_ID] " +
                    "from dbo.[Table] t inner join dbo.[Order] o on t.[Table_ID] = o.[Table_ID] " +
                    "where o.[Status] = 'A' ";

                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        int table_ID = Convert.ToInt32(reader["Table_ID"]);

                        DataRow row = result.NewRow();

                        row[0] = table_ID;

                        result.Rows.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        //orders

        /// <summary>
        /// Loading the active order when given a table number. Used in TablesForm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable LoadOrderDetailsByTableID(int? id)
        {
            DataTable result = new DataTable();

            result.Columns.Add("Order_ID");
            result.Columns.Add("Table_ID");
            result.Columns.Add("Name");
            result.Columns.Add("Qantity");
            result.Columns.Add("Price");
            result.Columns.Add("MenuItem_ID");

            SqlConnection connection = this.manipulator.GetConnection();

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "select o.[Order_ID], m.[Name], om.[Quantity], om.[MenuItem_ID], m.[Price] " +
                    "from dbo.[MenuItem] m inner join dbo.[OrderMenuItem] om on m.[MenuItem_ID] = om.[MenuItem_ID] " +
                    "inner join dbo.[Order] o on om.[Order_ID] = o.[Order_ID] inner join dbo.[Table] t on t.[Table_ID] = o.[Table_ID] " +
                    "where o.[Status] = 'A' and t.[Table_ID] = @Table_ID";

                SqlParameter param = null;

                param = new SqlParameter("@Table_ID", SqlDbType.Int);
                param.Value = id;
                command.Parameters.Add(param);


                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        int order_ID = Convert.ToInt32(reader["Order_ID"]);
                        int menuItem_ID = Convert.ToInt32(reader["MenuItem_ID"]);
                        string name = Convert.ToString(reader["Name"]);
                        int quantity = Convert.ToInt32(reader["Quantity"]);
                        double price = Convert.ToDouble(reader["Price"]);

                        DataRow row = result.NewRow();

                        row[0] = order_ID;
                        row[1] = id;
                        row[2] = name;
                        row[3] = quantity;
                        row[4] = price;
                        row[5] = menuItem_ID;

                        result.Rows.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        /// <summary>
        /// Loads orders by status - active or closed.
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public DataTable LoadOrders(char status)
        {
            DataTable result = new DataTable();

            result.Columns.Add("Order_ID");
            result.Columns.Add("Table_ID");

            SqlConnection connection = this.manipulator.GetConnection();

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "select [Order_ID], [Table_ID] from dbo.[Order] where [Status] = @Status order by [Order_ID] DESC";

                SqlParameter param = null;

                param = new SqlParameter("@Status", SqlDbType.Char);
                param.Value = status;
                command.Parameters.Add(param);

                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        int order_ID = Convert.ToInt32(reader["Order_ID"]);
                        int table_ID = Convert.ToInt32(reader["Table_ID"]);

                        DataRow row = result.NewRow();

                        row[0] = order_ID;
                        row[1] = table_ID;

                        result.Rows.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        /// <summary>
        /// Get's the order details by OrderID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public DataTable LoadOrderDetailsByOrderID(int id)
        {
            DataTable result = new DataTable();

            result.Columns.Add("Order_ID");
            result.Columns.Add("Table_ID");
            result.Columns.Add("Name");
            result.Columns.Add("Qantity");
            result.Columns.Add("Price");
            result.Columns.Add("MenuItem_ID");

            SqlConnection connection = this.manipulator.GetConnection();

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "select o.[Order_ID], o.[Table_ID], m.[Name], om.[Quantity], m.[Price], m.[MenuItem_ID] " +
                    "from dbo.[MenuItem] m inner join dbo.[OrderMenuItem] om on m.[MenuItem_ID] = om.[MenuItem_ID] " +
                    "inner join dbo.[Order] o on om.[Order_ID] = o.[Order_ID]" +
                    "where o.[Order_ID] = @Order_ID";

                SqlParameter param = null;

                param = new SqlParameter("@Order_ID", SqlDbType.Int);
                param.Value = id;
                command.Parameters.Add(param);


                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        int order_ID = Convert.ToInt32(reader["Order_ID"]);
                        int table_ID = Convert.ToInt32(reader["Table_ID"]);
                        string name = Convert.ToString(reader["Name"]);
                        int quantity = Convert.ToInt32(reader["Quantity"]);
                        double price = Convert.ToDouble(reader["Price"]);
                        int menuItem_ID = Convert.ToInt32(reader["MenuItem_ID"]);

                        DataRow row = result.NewRow();

                        row[0] = order_ID;
                        row[1] = table_ID;
                        row[2] = name;
                        row[3] = quantity;
                        row[4] = price;
                        row[5] = menuItem_ID;

                        result.Rows.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public int AddNewOrder(int table_ID, char status, int staffMember_ID)
        {
            SqlConnection connection = this.manipulator.GetConnection();

            int order_ID = -1;

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "insert into dbo.[Order] ([Table_ID], [Status], [StaffMember_ID]) " +
                    "values (@Table_ID, @Status, @StaffMember_ID); " +
                    "select SCOPE_IDENTITY() as [LastID] ;";

                SqlParameter param = null;

                param = new SqlParameter("@Table_ID", SqlDbType.Int);
                param.Value = table_ID;
                command.Parameters.Add(param);

                param = new SqlParameter("@Status", SqlDbType.Char);
                param.Value = status;
                command.Parameters.Add(param);

                param = new SqlParameter("@StaffMember_ID", SqlDbType.Int);
                param.Value = staffMember_ID;
                command.Parameters.Add(param);

                //command.ExecuteNonQuery();

                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        order_ID = Convert.ToInt32(reader["LastID"]);
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                connection.Close();
            }

            return order_ID;

        }

        public void AddNewOrderMenuItem(int order_ID, int menuItem_ID, int quantity)
        {
            SqlConnection connection = this.manipulator.GetConnection();

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "insert into dbo.[OrderMenuItem] ([Order_ID], [MenuItem_ID], [Quantity]) " +
                    "values (@Order_ID, @MenuItem_ID, @Quantity)";

                SqlParameter param = null;

                param = new SqlParameter("@Order_ID", SqlDbType.Int);
                param.Value = order_ID;
                command.Parameters.Add(param);

                param = new SqlParameter("@MenuItem_ID", SqlDbType.Int);
                param.Value = menuItem_ID;
                command.Parameters.Add(param);

                param = new SqlParameter("@Quantity", SqlDbType.Int);
                param.Value = quantity;
                command.Parameters.Add(param);

                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                connection.Close();
            }

        }

        public void UpdateOrder(int order_ID, int table_ID, char status)
        {
            SqlConnection connection = this.manipulator.GetConnection();

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "update dbo.[Order] " +
                                        "set [Table_ID] = @Table_ID, [Status] = @Status " +
                                        "where [Order_ID] = @Order_ID;";

                SqlParameter param = null;

                param = new SqlParameter("@Order_ID", SqlDbType.Int);
                param.Value = order_ID;
                command.Parameters.Add(param);

                param = new SqlParameter("@Table_ID", SqlDbType.Int);
                param.Value = table_ID;
                command.Parameters.Add(param);

                param = new SqlParameter("@Status", SqlDbType.Char);
                param.Value = status;
                command.Parameters.Add(param);

                command.ExecuteNonQuery();


            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                connection.Close();
            }

        }

        public void DeleteOrderMenuItem(int order_ID)
        {
            SqlConnection connection = this.manipulator.GetConnection();

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "delete from dbo.[OrderMenuItem] where [Order_ID] = @Order_ID;";

                SqlParameter param = null;

                param = new SqlParameter("@Order_ID", SqlDbType.Int);
                param.Value = order_ID;
                command.Parameters.Add(param);

                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                connection.Close();
            }

        }

        public void DeleteActiveOrder(int id)
        {
            SqlConnection connection = this.manipulator.GetConnection();

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "delete from dbo.[OrderMenuItem] where Order_ID = @Order_ID;" +
                                        "delete from dbo.[Order] where Order_ID = @Order_ID;";

                SqlParameter param = null;

                param = new SqlParameter("@Order_ID", SqlDbType.Int);
                param.Value = id;
                command.Parameters.Add(param);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                connection.Close();
            }

        }

        public void FinishOrder(int order_ID)
        {
            SqlConnection connection = this.manipulator.GetConnection();

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "update dbo.[Order] " +
                                        "set [Status] = 'C' " +
                                        "where [Order_ID] = @Order_ID;";

                SqlParameter param = null;

                param = new SqlParameter("@Order_ID", SqlDbType.Int);
                param.Value = order_ID;
                command.Parameters.Add(param);

                command.ExecuteNonQuery();


            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                connection.Close();
            }

        }

        //login

        public int CheckLoginAndRole(string username, string password)
        {
            bool login = false;
            int role = 0;

            SqlConnection connection = this.manipulator.GetConnection();
            try
            {
                connection.Open();
                SqlCommand command = this.manipulator.GetCommand();
                command.CommandText = "SELECT [Role_ID] FROM dbo.[Login] WHERE [Username] = @username AND [Password] = @password";
                SqlParameter param = null;
                param = new SqlParameter("@username", SqlDbType.VarChar);
                param.Value = username;
                command.Parameters.Add(param);
                param = new SqlParameter("@password", SqlDbType.VarChar);
                param.Value = password;
                command.Parameters.Add(param);
                SqlDataReader reader = command.ExecuteReader();
                using (reader)
                {

                    if (reader.Read())
                    {
                        login = true;
                        role = Convert.ToInt32(reader["Role_ID"]);
                    }
                }
            }
            finally
            {
                connection.Close();
            }

            return role;
        }

        /// <summary>
        /// Loads the different types of roles for the Add New User Form.
        /// </summary>
        /// <returns></returns>
        public DataTable LoadRoles()
        {
            DataTable result = new DataTable();

            result.Columns.Add("id");
            result.Columns.Add("name");

            SqlConnection connection = this.manipulator.GetConnection();

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "SELECT [Role_ID], [Name] FROM dbo.[Role] ORDER BY [Name] ASC";

                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["Role_ID"]);
                        string name = Convert.ToString(reader["Name"]);

                        DataRow row = result.NewRow();

                        row[0] = id;
                        row[1] = name;

                        result.Rows.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        /// <summary>
        /// Adds new user to the database.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="role_id"></param>
        public void AddNewUser(string username, string password, int role_id)
        {
            SqlConnection connection = this.manipulator.GetConnection();

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "insert into dbo.[Login] ([Username], [Password], [Role_ID]) values (@Username, @Password, @Role_ID)";

                SqlParameter param = null;

                param = new SqlParameter("@Username", SqlDbType.VarChar);
                param.Value = username;
                command.Parameters.Add(param);

                param = new SqlParameter("@Password", SqlDbType.VarChar);
                param.Value = password;
                command.Parameters.Add(param);

                param = new SqlParameter("@Role_ID", SqlDbType.Int);
                param.Value = role_id;
                command.Parameters.Add(param);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Loading the full menu by types in MenuForm.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable LoadMenuItemsByType(string type)
        {
            DataTable result = new DataTable();

            result.Columns.Add("MenuItem_ID");
            result.Columns.Add("Name");


            SqlConnection connection = this.manipulator.GetConnection();

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "select * from dbo.[MenuItem] where [Type] = @Type";

                SqlParameter param = null;

                param = new SqlParameter("@Type", SqlDbType.VarChar);
                param.Value = type;

                command.Parameters.Add(param);

                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        int menuItem_ID = Convert.ToInt32(reader["MenuItem_ID"]);
                        string name = Convert.ToString(reader["Name"]);


                        DataRow row = result.NewRow();

                        row[0] = menuItem_ID;
                        row[1] = name;


                        result.Rows.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        /// <summary>
        /// Loads ManuItem for MenuItemForm.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Entities.MenuItem LoadMenuItemByName(string name)
        {
            Entities.MenuItem result = null;

            //if (name == null)
            //{
            //    result = new Entities.MenuItem();
            //    result.MenuItem_ID = 0;
            //    result.Name = "";
            //    result.Type = "";
            //    result.Price = 0;
            //    result.Quantity = "";
            //    result.Description = "";
            //}



            SqlConnection connection = this.manipulator.GetConnection();

            connection.Open();

            SqlCommand command = this.manipulator.GetCommand();
            command.CommandText = "select * from dbo.[MenuItem] where [Name] = @Name";

            SqlParameter param = null;
            param = new SqlParameter("@Name", SqlDbType.VarChar);
            param.Value = name;
            command.Parameters.Add(param);

            SqlDataReader reader = command.ExecuteReader();

            using (reader)
            {
                if (reader.Read())
                {
                    result = new Entities.MenuItem();
                    result.MenuItem_ID = Convert.ToInt32(reader["MenuItem_ID"]);
                    result.Name = Convert.ToString(reader["Name"]);
                    result.Type = Convert.ToString(reader["Type"]);
                    result.Price = Convert.ToDouble(reader["Price"]);
                    result.Quantity = Convert.ToString(reader["Quantity"]);
                    result.Description = Convert.ToString(reader["Description"]);

                }
            }
            connection.Close();


            return result;
        }

        /// <summary>
        /// Updates MenuItem from MenuItemForm.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="price"></param>
        /// <param name="quantity"></param>
        /// <param name="description"></param>
        public void UpdateMenuItem(int id, string name, string type, double price, string quantity, string description)
        {
            SqlConnection connection = this.manipulator.GetConnection();
            connection.Open();

            SqlCommand command = this.manipulator.GetCommand();
            command.CommandText = "UPDATE dbo.[MenuItem] SET [Name] = @Name, [Type] = @Type, [Price] = @Price, [Quantity] = @Quantity, [Description] = @Description " +
                "WHERE [MenuItem_ID] = @MenuItem_ID";

            SqlParameter param = null;

            param = new SqlParameter("@MenuItem_ID", SqlDbType.Int);
            param.Value = id;
            command.Parameters.Add(param);

            param = new SqlParameter("@Name", SqlDbType.VarChar);
            param.Value = name;
            command.Parameters.Add(param);

            param = new SqlParameter("@Type", SqlDbType.VarChar);
            param.Value = type;
            command.Parameters.Add(param);

            param = new SqlParameter("@Price", SqlDbType.Decimal);
            param.Value = price;
            command.Parameters.Add(param);

            param = new SqlParameter("@Quantity", SqlDbType.VarChar);
            param.Value = quantity;
            command.Parameters.Add(param);

            param = new SqlParameter("@Description", SqlDbType.VarChar);
            param.Value = description;
            command.Parameters.Add(param);

            command.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Create new menu item.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="price"></param>
        /// <param name="quantity"></param>
        /// <param name="description"></param>
        public void CreateMenuItem(string name, string type, double price, string quantity, string description)
        {
            SqlConnection connection = this.manipulator.GetConnection();

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "insert into dbo.[MenuItem] ([Name], [Type], [Price], [Quantity], [Description]) " +
                    "values (@Name, @Type, @Price, @Quantity, @Description)";

                SqlParameter param = null;

                param = new SqlParameter("@Name", SqlDbType.VarChar);
                param.Value = name;
                command.Parameters.Add(param);

                param = new SqlParameter("@Type", SqlDbType.VarChar);
                param.Value = type;
                command.Parameters.Add(param);

                param = new SqlParameter("@Price", SqlDbType.Decimal);
                param.Value = price;
                command.Parameters.Add(param);

                param = new SqlParameter("@Quantity", SqlDbType.VarChar);
                param.Value = quantity;
                command.Parameters.Add(param);

                param = new SqlParameter("@Description", SqlDbType.VarChar);
                param.Value = description;
                command.Parameters.Add(param);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Deletes MenuItem
        /// </summary>
        /// <param name="id"></param>
        public void DeleteMenuItem(int id)
        {
            SqlConnection connection = this.manipulator.GetConnection();

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "delete from dbo.[OrderMenuItem] where [MenuItem_ID] = @MenuItem_ID; " +
                    "delete from dbo.[MenuItem] where MenuItem_ID = @MenuItem_ID";

                SqlParameter param = null;

                param = new SqlParameter("@MenuItem_ID", SqlDbType.Int);
                param.Value = id;
                command.Parameters.Add(param);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                connection.Close();
            }

        }

        //staffMember

        public DataTable LoadStaffMembers()
        {
            DataTable result = new DataTable();

            result.Columns.Add("staffMember_ID");
            result.Columns.Add("displayName");

            SqlConnection connection = this.manipulator.GetConnection();

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "SELECT [StaffMember_ID], [DisplayName] FROM dbo.[StaffMember] ";

                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        int staffMemeber_ID = Convert.ToInt32(reader["StaffMember_ID"]);
                        string displayName = Convert.ToString(reader["DisplayName"]);

                        DataRow row = result.NewRow();

                        row[0] = staffMemeber_ID;
                        row[1] = displayName;

                        result.Rows.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public StaffMember LoadStaffMembersByStaffMemberID(int staffMember_ID)
        {
            StaffMember result = null;

            SqlConnection connection = this.manipulator.GetConnection();

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "select [StaffMember_ID], [FirstName], [MiddleName], [LastName], [DisplayName], [Image] " +
                    "from dbo.[StaffMember] " +
                    "where [StaffMember_ID] = @StaffMember_ID ";

                SqlParameter param = null;
                param = new SqlParameter("@StaffMember_ID", SqlDbType.Int);
                param.Value = staffMember_ID;
                command.Parameters.Add(param);

                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        result = new StaffMember();

                        
                        string firstName = Convert.ToString(reader["FirstName"]);
                        string middleName = Convert.ToString(reader["MiddleName"]);
                        string lastName = Convert.ToString(reader["LastName"]);
                        string displayName = Convert.ToString(reader["DisplayName"]);

                        byte[] image;
                        if (reader["Image"] != DBNull.Value)
                        {
                            image = ((byte[])reader["Image"]);
                        }
                        else
                        {
                            image = null;
                        }
                        

                        result.StaffMember_ID = staffMember_ID;
                        result.FirstName = firstName;
                        result.MiddleName = middleName;
                        result.LastName = lastName;
                        result.DisplayName = displayName;
                        result.Image = image;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public StaffMember LoadStaffMembersByOrderID(int order_ID)
        {
            StaffMember result = null;

            SqlConnection connection = this.manipulator.GetConnection();

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "select sm.[StaffMember_ID], sm.[FirstName], sm.[MiddleName], sm.[LastName], sm.[DisplayName] " +
                    "from dbo.[StaffMember] sm inner join dbo.[Order] o on sm.[StaffMember_ID] = o.[StaffMember_ID] " +
                    "where o.[Order_ID] = @Order_ID ";

                SqlParameter param = null;
                param = new SqlParameter("@Order_ID", SqlDbType.Int);
                param.Value = order_ID;
                command.Parameters.Add(param);

                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        result = new StaffMember();

                        int staffMember_ID = Convert.ToInt32(reader["StaffMember_ID"]);
                        string firstName = Convert.ToString(reader["FirstName"]);
                        string middleName = Convert.ToString(reader["MiddleName"]);
                        string lastName = Convert.ToString(reader["LastName"]);
                        string displayName = Convert.ToString(reader["DisplayName"]);
                        

                        result.StaffMember_ID = staffMember_ID;
                        result.FirstName = firstName;
                        result.MiddleName = middleName;
                        result.LastName = lastName;
                        result.DisplayName = displayName;
                      
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public void AddStaffMember(string firstName, string middleName, string lastName, string displayName, byte[] image)
        {
            SqlConnection connection = this.manipulator.GetConnection();

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "insert into dbo.[StaffMember] ([FirstName], [MiddleName], [LastName], [DisplayName], [Image]) " +
                    "values (@FirstName, @MiddleName, @LastName, @DisplayName, @Image) ";

                SqlParameter param = null;

                param = new SqlParameter("@FirstName", SqlDbType.VarChar);
                param.Value = firstName;
                command.Parameters.Add(param);

                param = new SqlParameter("@MiddleName", SqlDbType.VarChar);
                param.Value = middleName;
                command.Parameters.Add(param);

                param = new SqlParameter("@LastName", SqlDbType.VarChar);
                param.Value = lastName;
                command.Parameters.Add(param);

                param = new SqlParameter("@DisplayName", SqlDbType.VarChar);
                param.Value = displayName;
                command.Parameters.Add(param);
                
                
                
                param = new SqlParameter("@Image", SqlDbType.VarBinary);
                param.Value = image;
                command.Parameters.Add(param);
                
                

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateStaffMember(int staffMember_ID, string firstName, string middleName, string lastName, string displayName, byte[] image)
        {
            SqlConnection connection = this.manipulator.GetConnection();

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "update dbo.[StaffMember] " +
                    "SET [FirstName] = @FirstName, [MiddleName] = @MiddleName, [LastName] = @LastName, [DisplayName] = @DisplayName, [Image] = @Image " +
                    "WHERE [StaffMember_ID] = @StaffMember_ID";

                SqlParameter param = null;

                param = new SqlParameter("@StaffMember_ID", SqlDbType.Int);
                param.Value = staffMember_ID;
                command.Parameters.Add(param);

                param = new SqlParameter("@FirstName", SqlDbType.VarChar);
                param.Value = firstName;
                command.Parameters.Add(param);

                param = new SqlParameter("@MiddleName", SqlDbType.VarChar);
                param.Value = middleName;
                command.Parameters.Add(param);

                param = new SqlParameter("@LastName", SqlDbType.VarChar);
                param.Value = lastName;
                command.Parameters.Add(param);

                param = new SqlParameter("@DisplayName", SqlDbType.VarChar);
                param.Value = displayName;
                command.Parameters.Add(param);

                param = new SqlParameter("@Image", SqlDbType.VarBinary);
                param.Value = image;
                command.Parameters.Add(param);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeleteStaffMember(int staffMember_ID)
        {
            SqlConnection connection = this.manipulator.GetConnection();

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "delete from dbo.[StaffMember] where [StaffMember_ID] = @StaffMember_ID";

                SqlParameter param = null;

                param = new SqlParameter("@StaffMember_ID", SqlDbType.Int);
                param.Value = staffMember_ID;
                command.Parameters.Add(param);

                command.ExecuteNonQuery();

                MessageBox.Show("Succesfully deleted.");
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
                MessageBox.Show("This staff member cannot be deleted because their name is a part of orders.");
            }
            finally
            {
                connection.Close();
            }

        }

        //search

        public DataTable SearchOrdersArchiveByTable_ID(int table_ID)
        {
            DataTable result = new DataTable();

            result.Columns.Add("Order_ID");
            
            SqlConnection connection = this.manipulator.GetConnection();

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "select [Order_ID] from dbo.[Order] " +
                    "where [Status] = 'C' and [Table_ID] = @Table_ID";

                SqlParameter param = null;

                param = new SqlParameter("@Table_ID", SqlDbType.Int);
                param.Value = table_ID;
                command.Parameters.Add(param);

                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        int order_ID = Convert.ToInt32(reader["Order_ID"]);
                        
                        DataRow row = result.NewRow();

                        row[0] = order_ID;
                        
                        result.Rows.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public DataTable SearchOrdersArchiveByStaffMemberName(string name)
        {
            DataTable result = new DataTable();

            result.Columns.Add("Order_ID");

            SqlConnection connection = this.manipulator.GetConnection();

            try
            {
                connection.Open();

                SqlCommand command = this.manipulator.GetCommand();

                command.CommandText = "select o.[Order_ID] " +
                    "from dbo.[Order] o inner join dbo.[StaffMember] sm on o.[StaffMember_ID] = sm.[StaffMember_ID] " +
                    "where o.[Status] = 'C' " +
                    "and (sm.[DisplayName] like @Param " +
                    "or sm.[FirstName] like @Param " +
                    "or sm.[MiddleName] like @Param " +
                    "or sm.[LastName] like @Param) ";

                SqlParameter param = null;

                param = new SqlParameter("@Param", SqlDbType.VarChar);
                param.Value = "%"+name+"%";
                command.Parameters.Add(param);

                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        int order_ID = Convert.ToInt32(reader["Order_ID"]);

                        DataRow row = result.NewRow();

                        row[0] = order_ID;

                        result.Rows.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                connection.Close();
            }

            return result;
        }
    }
}
