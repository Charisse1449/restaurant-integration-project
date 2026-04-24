using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestaurantPOS.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS
{
    class Configurator
    {
        private DBManipulator manipulator;

        public Configurator()
        {
            this.manipulator = new DBManipulator();
        }

        private JArray ExtractArrayFromApiResponse(string json)
        {
            JToken token = JToken.Parse(json);

            if (token.Type == JTokenType.Array)
                return (JArray)token;

            if (token["data"] != null)
            {
                if (token["data"].Type == JTokenType.Array)
                    return (JArray)token["data"];

                if (token["data"]["data"] != null && token["data"]["data"].Type == JTokenType.Array)
                    return (JArray)token["data"]["data"];
            }

            if (token["orders"] != null && token["orders"].Type == JTokenType.Array)
                return (JArray)token["orders"];

            if (token["recipes"] != null && token["recipes"].Type == JTokenType.Array)
                return (JArray)token["recipes"];

            if (token["tables"] != null && token["tables"].Type == JTokenType.Array)
                return (JArray)token["tables"];

            throw new Exception("Cannot find array in API response: " + json);
        }

        /// <summary>
        /// Loads all tables in the Tables form.
        /// </summary>
        /// <returns></returns>
        public DataTable LoadTables()
        {
            DataTable result = new DataTable();
            result.Columns.Add("Table_ID");

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = "http://127.0.0.1:8000/api/tables";
                    string json = client.GetStringAsync(url).Result;

                    JToken token = JToken.Parse(json);

                    JArray tables;

                    if (token.Type == JTokenType.Array)
                    {
                        tables = (JArray)token;
                    }
                    else
                    {
                        tables = (JArray)token["data"];
                    }

                    foreach (var table in tables)
                    {
                        DataRow row = result.NewRow();
                        row["Table_ID"] = table["number"].ToString();
                        result.Rows.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("API Error LoadTables:\n" + e.Message);
            }

            return result;
        }

        public DataTable LoadActiveTables()
        {
            DataTable result = new DataTable();
            result.Columns.Add("Table_ID");

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = "http://127.0.0.1:8000/api/tables";
                    string json = client.GetStringAsync(url).Result;

                    JToken token = JToken.Parse(json);

                    JArray tables;

                    if (token.Type == JTokenType.Array)
                    {
                        tables = (JArray)token;
                    }
                    else
                    {
                        tables = (JArray)token["data"];
                    }

                    foreach (var table in tables)
                    {
                        string status = table["status"].ToString();

                        if (status == "occupied")
                        {
                            DataRow row = result.NewRow();
                            row["Table_ID"] = table["number"].ToString();
                            result.Rows.Add(row);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("API Error LoadActiveTables:\n" + e.Message);
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
            return new DataTable(); // TEMP
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

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = "http://127.0.0.1:8000/api/orders";
                    string json = client.GetStringAsync(url).Result;

                    JArray orders = ExtractArrayFromApiResponse(json);

                    foreach (var order in orders)
                    {
                        string apiStatus = order["status"]?.ToString();

                        bool include = false;

                        // Old desktop: A = active, C = closed
                        // Laravel: new/preparing/ready/completed
                        if (status == 'A' && apiStatus != "completed")
                        {
                            include = true;
                        }
                        else if (status == 'C' && apiStatus == "completed")
                        {
                            include = true;
                        }

                        if (include)
                        {
                            DataRow row = result.NewRow();
                            row["Order_ID"] = order["id"].ToString();
                            row["Table_ID"] = order["table_number"]?.ToString();
                            result.Rows.Add(row);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("API Error LoadOrders:\n" + e.Message);
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
            return new DataTable(); // TEMP
        }

        public int AddNewOrder(int table_ID, char status, int staffMember_ID)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var data = new
                    {
                        type = "dine-in",
                        table_number = table_ID,
                        subtotal = 0,
                        tax = 0,
                        delivery_fee = 0,
                        total = 0,
                        status = "new"
                    };

                    string jsonData = JsonConvert.SerializeObject(data);
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    var response = client.PostAsync("http://127.0.0.1:8000/api/orders", content).Result;

                    string result = response.Content.ReadAsStringAsync().Result;

                    JObject obj = JObject.Parse(result);

                    return (int)obj["id"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("API Error AddNewOrder:\n" + ex.Message);
                return -1;
            }
        }

        public void AddNewOrderMenuItem(int order_ID, int menuItem_ID, int quantity)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var data = new
                    {
                        recipe_id = menuItem_ID,
                        quantity = quantity,
                        name = "Item",
                        price = 0,
                        total = 0
                    };

                    string jsonData = JsonConvert.SerializeObject(data);
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    client.PostAsync($"http://127.0.0.1:8000/api/orders/{order_ID}/items", content).Wait();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("API Error AddOrderItem:\n" + ex.Message);
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
            // TEMPORARY BYPASS while integrating Laravel API
            // 1 = admin role
            return 1;
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

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = "http://127.0.0.1:8000/api/recipes";
                    string json = client.GetStringAsync(url).Result;

                    JArray recipes = ExtractArrayFromApiResponse(json);

                    foreach (var recipe in recipes)
                    {
                        string category = recipe["category"]?.ToString();

                        // If your old C# menu type does not match Laravel category,
                        // still show all for now.
                        if (string.IsNullOrEmpty(type) || category == type)
                        {
                            DataRow row = result.NewRow();
                            row["MenuItem_ID"] = recipe["id"].ToString();
                            row["Name"] = recipe["name"].ToString();
                            result.Rows.Add(row);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("API Error LoadMenuItemsByType:\n" + e.Message);
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
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string json = client.GetStringAsync("http://127.0.0.1:8000/api/recipes").Result;
                    JArray recipes = ExtractArrayFromApiResponse(json);

                    foreach (var recipe in recipes)
                    {
                        if (recipe["name"]?.ToString() == name)
                        {
                            return new Entities.MenuItem
                            {
                                MenuItem_ID = Convert.ToInt32(recipe["id"]),
                                Name = recipe["name"]?.ToString(),
                                Type = recipe["category"]?.ToString(),
                                Price = Convert.ToDouble(recipe["price"]),
                                Quantity = "1",
                                Description = recipe["instructions"]?.ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("API Error LoadMenuItemByName:\n" + ex.Message);
            }

            return new Entities.MenuItem
            {
                MenuItem_ID = 0,
                Name = "",
                Type = "",
                Price = 0,
                Quantity = "1",
                Description = ""
            };
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
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var data = new
                    {
                        name = name,
                        category = type,
                        price = price,
                        base_portions = 1,
                        prep_time = 10,
                        cook_time = 10,
                        difficulty = "easy",
                        ingredients = new string[] { },
                        instructions = description,
                        is_active = true
                    };

                    string jsonData = JsonConvert.SerializeObject(data);
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    var response = client.PutAsync($"http://127.0.0.1:8000/api/recipes/{id}", content).Result;
                    string responseBody = response.Content.ReadAsStringAsync().Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("API Error UpdateMenuItem:\n" + response.StatusCode + "\n" + responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("API Error UpdateMenuItem:\n" + ex.Message);
            }
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
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var data = new
                    {
                        name = name,
                        category = type,
                        price = price,
                        base_portions = 1,
                        prep_time = 10,
                        cook_time = 10,
                        difficulty = "easy",
                        ingredients = new string[] { },
                        instructions = description,
                        is_active = true
                    };

                    string jsonData = JsonConvert.SerializeObject(data);
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    var response = client.PostAsync("http://127.0.0.1:8000/api/recipes", content).Result;
                    string result = response.Content.ReadAsStringAsync().Result;

                    MessageBox.Show("Status: " + response.StatusCode + "\n" + result);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Menu item added successfully!");
                    }
                    else
                    {
                        MessageBox.Show("API Error: " + response.StatusCode + "\n" + result);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("API Error CreateMenuItem: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Deletes MenuItem
        /// </summary>
        /// <param name="id"></param>
        public void DeleteMenuItem(int id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = client.DeleteAsync($"http://127.0.0.1:8000/api/recipes/{id}").Result;
                    string responseBody = response.Content.ReadAsStringAsync().Result;

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Successfully deleted.");
                    }
                    else
                    {
                        MessageBox.Show("API Error DeleteMenuItem:\n" + response.StatusCode + "\n" + responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("API Error DeleteMenuItem:\n" + ex.Message);
            }
        }

        //staffMember

        public DataTable LoadStaffMembers()
        {
            DataTable result = new DataTable();

            result.Columns.Add("staffMember_ID");
            result.Columns.Add("displayName");

            DataRow row = result.NewRow();
            row["staffMember_ID"] = 1;
            row["displayName"] = "Default Staff";
            result.Rows.Add(row);

            return result;
        }

        public StaffMember LoadStaffMembersByStaffMemberID(int staffMember_ID)
        {
            StaffMember result = new StaffMember();

            result.StaffMember_ID = staffMember_ID;
            result.FirstName = "Default";
            result.MiddleName = "";
            result.LastName = "Staff";
            result.DisplayName = "Default Staff";
            result.Image = null;

            return result;
        }

        public StaffMember LoadStaffMembersByOrderID(int order_ID)
        {
            StaffMember result = new StaffMember();

            result.StaffMember_ID = 1;
            result.FirstName = "Default";
            result.MiddleName = "";
            result.LastName = "Staff";
            result.DisplayName = "Default Staff";
            result.Image = null;

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
                param.Value = "%" + name + "%";
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
