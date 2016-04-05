using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datebase
{
    public class DB
    {
        private string connectionString;

        public DB() { }

        public DB(string conString)
        {
            connectionString = conString;
        }

        public string ConnectionString
        {
            private get { return connectionString;  }
            set { connectionString = value; }
        }


        public ObservableCollection<DeliveryType> GetDeliveryType()
        {

            ObservableCollection<DeliveryType> list = new ObservableCollection<DeliveryType>();
            String query = "exec GetDeliveryType";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmdCheck = new SqlCommand(query, conn);
                    SqlDataReader reader = cmdCheck.ExecuteReader();

                    while (reader.Read())
                    {
                        DeliveryType dt = new DeliveryType();

                        dt.Id = (int)reader[0];
                        dt.Name = reader.GetString(1).Trim();

                        list.Add(dt);
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("błąd GetAllNaczepy: " + query + "\n \n" + e);
                Debug.WriteLine("błąd GetDeliveryType: " + query + "\n \n" + e);
            }

            return list;
        }

        public bool AddEditDeliveryType(DeliveryType dt)
        {
            bool isAdded = false;
            String query = "exec AddEditDeliveryType " + dt.Id + ", '" + dt.Name + "'";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmdCheck = new SqlCommand(query, conn);
                    SqlDataReader reader = cmdCheck.ExecuteReader();

                    string temp;
                    while (reader.Read())
                    {

                        temp = reader.GetString(1).Trim();

                        if (temp == "") // jeśli rekord został poprawnie dodany/zmieniony to informacja zwrotna jest pusta
                            isAdded = true;
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("błąd AddEditDeliveryType: " + query + "\n \n" + e);
            }
            return isAdded;
        }

        public Status DeleteDeliveryType(DeliveryType dt)
        {
            Status stat = new Status();
            String query = "exec DeleteDeliveryType " + dt.Id;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmdCheck = new SqlCommand(query, conn);
                    SqlDataReader reader = cmdCheck.ExecuteReader();


                    while (reader.Read())
                    {
                        stat.Id = (int)reader[0];
                        stat.Info = reader.GetString(1).Trim();

                        if (stat.Id == 1) // jeśli rekord został poprawnie dodany/zmieniony to Id == 1
                            stat.State = true;
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("błąd DeleteDeliveryType: " + query + "\n \n" + e);
            }
            return stat;
        }



        public ObservableCollection<ProductType> GetProductType()
        {

            ObservableCollection<ProductType> list = new ObservableCollection<ProductType>();
            String query = "exec GetProductType";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmdCheck = new SqlCommand(query, conn);
                    SqlDataReader reader = cmdCheck.ExecuteReader();

                    while (reader.Read())
                    {
                        ProductType pt = new ProductType();

                        pt.Id = (int)reader[0];
                        pt.Name = reader.GetString(1).Trim();

                        list.Add(pt);
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("błąd GetProductType: " + query + "\n \n" + e);
            }

            return list;
        }

        public bool AddEditProductType(ProductType pt)
        {
            bool isAdded = false;
            String query = "exec AddEditProductType " + pt.Id +", '" + pt.Name +"'";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmdCheck = new SqlCommand(query, conn);
                    SqlDataReader reader = cmdCheck.ExecuteReader();

                    string temp;
                    while (reader.Read())
                    {
                        
                        temp = reader.GetString(1).Trim();

                        if (temp == "") // jeśli rekord został poprawnie dodany/zmieniony to informacja zwrotna jest pusta
                            isAdded = true;
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("błąd AddEditProductType: " + query + "\n \n" + e);
            }
            return isAdded;
        }

        public Status DeleteProductType(ProductType pt)
        {
            Status stat = new Status();
            String query = "exec DeleteProductType " + pt.Id;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmdCheck = new SqlCommand(query, conn);
                    SqlDataReader reader = cmdCheck.ExecuteReader();

                    
                    while (reader.Read())
                    {
                        stat.Id = (int)reader[0];
                        stat.Info = reader.GetString(1).Trim();

                        if (stat.Id == 1) // jeśli rekord został poprawnie dodany/zmieniony to Id == 1
                            stat.State = true;
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("błąd DeleteProductType: " + query + "\n \n" + e);
            }
            return stat;
        }



        public ObservableCollection<Order> GetOrder()
        {

            ObservableCollection<Order> list = new ObservableCollection<Order>();
            String query = "exec GetOrder";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmdCheck = new SqlCommand(query, conn);
                    SqlDataReader reader = cmdCheck.ExecuteReader();

                    while (reader.Read())
                    {
                        Order o = new Order();

                        o.Id = (int)reader[0];

                        o.DateCreate = reader.GetDateTime(1);

                        o.CustomerName = reader.GetString(2).Trim();
                        o.CustomerSurname = reader.GetString(3).Trim();
                        o.CustomerAdress = reader.GetString(4).Trim();
                        o.DeliveryTypeId = (int)reader[5];

                        if (reader[6] != DBNull.Value)
                            o.DateRealization = reader.GetDateTime(6);

                        if (reader[7] != DBNull.Value)
                            o.Realization = reader.GetBoolean(7);

                        list.Add(o);
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("błąd GetAllNaczepy: " + query + "\n \n" + e);
                Debug.WriteLine("błąd GetGetOrder: " + query + "\n \n" + e);
            }

            return list;
        }
        
        public Status DeleteOrder(Order pt)
        {
            Status stat = new Status();
            String query = "exec DeleteOrder " + pt.Id;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmdCheck = new SqlCommand(query, conn);
                    SqlDataReader reader = cmdCheck.ExecuteReader();


                    while (reader.Read())
                    {
                        stat.Id = (int)reader[0];
                        stat.Info = reader.GetString(1).Trim();

                        if (stat.Id == 1) // jeśli rekord został poprawnie dodany/zmieniony to Id == 1
                            stat.State = true;
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("błąd DeleteOrder: " + query + "\n \n" + e);
            }
            return stat;
        }

        public Status RealizeOrder(Order pt)
        {
            Status stat = new Status();
            String query = "exec RealizeOrder " + pt.Id;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmdCheck = new SqlCommand(query, conn);
                    SqlDataReader reader = cmdCheck.ExecuteReader();


                    while (reader.Read())
                    {
                        stat.Id = (int)reader[0];
                        stat.Info = reader.GetString(1).Trim();

                        if (stat.Id == 1) // jeśli rekord został poprawnie dodany/zmieniony to Id == 1
                            stat.State = true;
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("błąd RealizeOrder: " + query + "\n \n" + e);
            }
            return stat;
        }

        public Status AddOrder(Order o)
        {
            Status stat = new Status();
            String query = "exec AddOrder ";

            try
            {
                query = "exec AddOrder '" + o.CustomerName + "', '" + o.CustomerSurname + "', '" + o.CustomerAdress + "', " + o.DeliveryType.Id;
            }
            catch(Exception)
            {
                stat.Info = "Popraw dane";
                return stat;
            }

            try
            {
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmdCheck = new SqlCommand(query, conn);
                    SqlDataReader reader = cmdCheck.ExecuteReader();


                    while (reader.Read())
                    {
                        var a = reader[0];
                        stat.Id = (int)reader[0];
                        var x = reader[0];
                        stat.Info = reader.GetString(1).Trim();

                        if (stat.Info == "") 
                            stat.State = true;
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("błąd AddOrder: " + query + "\n \n" + e);
            }
            return stat;
        }

        public Status AddProductToOrder(int productId, int orderId, int addCount)
        {
            Status stat = new Status();
            String query = "exec AddProductToOrder ";

            try
            {
                query = "exec AddProductToOrder " + productId + ", " + addCount + ", " + orderId;
            }
            catch (Exception)
            {
                stat.Info = "Popraw dane";
                return stat;
            }

            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmdCheck = new SqlCommand(query, conn);
                    SqlDataReader reader = cmdCheck.ExecuteReader();


                    while (reader.Read())
                    {
                        var a = reader[0];
                        stat.Id = (int)reader[0];
                        var x = reader[0];
                        stat.Info = reader.GetString(1).Trim();

                        if (stat.Id == 1)
                            stat.State = true;
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("błąd AddOrder: " + query + "\n \n" + e);
            }
            return stat;
        }

        public ObservableCollection<Product> GetProductListFromOrder(int idOrder)
        {

            ObservableCollection<Product> list = new ObservableCollection<Product>();
            String query = "exec GetProductListFromOrder " + idOrder;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmdCheck = new SqlCommand(query, conn);
                    SqlDataReader reader = cmdCheck.ExecuteReader();

                    while (reader.Read())
                    {
                        Product p = new Product();

                        p.Id = (int)reader[0];
                        p.Barecode = reader.GetString(1);
                        p.Name = reader.GetString(2);

                        p.DateWarianty = reader.GetDateTime(3);
                        p.ProductTypeId = (int)reader[4];

                        if (reader[6] != DBNull.Value)
                            p.Count = (int)reader[6];

                        list.Add(p);
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("błąd GetAllNaczepy: " + query + "\n \n" + e);
                Debug.WriteLine("błąd GetProductListFromOrder: " + query + "\n \n" + e);
            }

            return list;
        }


        public ObservableCollection<Product> GetProduct()
        {

            ObservableCollection<Product> list = new ObservableCollection<Product>();
            String query = "exec GetProduct";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmdCheck = new SqlCommand(query, conn);
                    SqlDataReader reader = cmdCheck.ExecuteReader();

                    while (reader.Read())
                    {
                        Product p = new Product();

                        p.Id = (int)reader[0];
                        p.Barecode = reader.GetString(1);
                        p.Name = reader.GetString(2);

                        p.DateWarianty = reader.GetDateTime(3);
                        p.ProductTypeId = (int)reader[4];

                        if (reader[6] != DBNull.Value)
                            p.Count = (int)reader[6];

                        if (reader[7] != DBNull.Value)
                            p.CountReserved = (int)reader[7];

                        if (reader[8] != DBNull.Value)
                            p.CountSold = (int)reader[8];

                        list.Add(p);
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("błąd GetAllNaczepy: " + query + "\n \n" + e);
                Debug.WriteLine("błąd GetProduct: " + query + "\n \n" + e);
            }

            return list;
        }

        public Status AddEditProduct(Product pr)
        {
           // bool isAdded = false;
            String query = "exec AddEditProduct ";
            Status stat = new Status();
            try
            {
                query = "exec AddEditProduct " + pr.Id + ", '" + pr.Name + "', '" + pr.Barecode + "', '" + pr.DateWarianty.ToString() + "', " + pr.ProductType.Id;
            }
            catch (Exception)
            {
                stat.Info = "Popraw dane";
                return stat;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmdCheck = new SqlCommand(query, conn);
                    SqlDataReader reader = cmdCheck.ExecuteReader();

                    while (reader.Read())
                    {


                        stat.Id = (int)reader[0];
                        stat.Info = reader.GetString(1).Trim();

                        if (stat.Info == "") // jeśli rekord został poprawnie dodany/zmieniony to informacja zwrotna jest pusta
                            stat.State = true;
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("błąd AddEditProduct: " + query + "\n \n" + e);
            }
            return stat;
        }

        public Status DeleteProduct(Product pr)
        {
            Status stat = new Status();
            String query = "exec DeleteProduct " + pr.Id;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmdCheck = new SqlCommand(query, conn);
                    SqlDataReader reader = cmdCheck.ExecuteReader();


                    while (reader.Read())
                    {
                        stat.Id = (int)reader[0];
                        stat.Info = reader.GetString(1).Trim();

                        if (stat.Id == 1) // jeśli rekord został poprawnie dodany/zmieniony to Id == 1
                            stat.State = true;
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("błąd DeleteProduct: " + query + "\n \n" + e);
            }
            return stat;
        }

        public bool AddOneProduct(int id, int count)
        {
            bool isAdded = false;
            String query = "exec AddOneProduct " + id + " , " + count;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmdCheck = new SqlCommand(query, conn);
                    SqlDataReader reader = cmdCheck.ExecuteReader();

                    string temp;
                    while (reader.Read())
                    {

                        temp = reader.GetString(1).Trim();

                        if (temp == "") // jeśli rekord został poprawnie dodany/zmieniony to informacja zwrotna jest pusta
                            isAdded = true;
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("błąd AddEditCustomer: " + query + "\n \n" + e);
            }
            return isAdded;
        }

        public Status DeleteOneProduct(int id)
        {
            Status stat = new Status();
            String query = "exec DeleteOneProduct " + id;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmdCheck = new SqlCommand(query, conn);
                    SqlDataReader reader = cmdCheck.ExecuteReader();


                    while (reader.Read())
                    {
                        stat.Id = (int)reader[0];
                        stat.Info = reader.GetString(1).Trim();

                        if (stat.Id == 1) // jeśli rekord został poprawnie dodany/zmieniony to Id == 1
                            stat.State = true;
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("błąd DeleteProduct: " + query + "\n \n" + e);
            }
            return stat;
        }


        public ObservableCollection<OneProduct> GetProductList(Product pr)
        {

            ObservableCollection<OneProduct> list = new ObservableCollection<OneProduct>();
            String query = "exec GetProductList " + pr.Id;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmdCheck = new SqlCommand(query, conn);
                    SqlDataReader reader = cmdCheck.ExecuteReader();

                    int i = 1;
                    while (reader.Read())
                    {
                        OneProduct op = new OneProduct();

                        op.Id = (int)reader[0];
                        op.ProductId = (int)reader[1];
                        op.Nr = i;
                        list.Add(op);
                        i++;
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("błąd GetAllNaczepy: " + query + "\n \n" + e);
                Debug.WriteLine("błąd GetProductList: " + query + "\n \n" + e);
            }

            return list;
        }

        public ObservableCollection<Customer> GetCustomer()
        {

            ObservableCollection<Customer> list = new ObservableCollection<Customer>();
            String query = "exec GetCustomer";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmdCheck = new SqlCommand(query, conn);
                    SqlDataReader reader = cmdCheck.ExecuteReader();

                    while (reader.Read())
                    {
                        Customer c = new Customer();

                        c.Id = (int)reader[0];
                        c.Name = reader.GetString(1);
                        c.Surname = reader.GetString(2);
                        c.Tel = reader.GetString(3);
                        c.Street = reader.GetString(4);
                        c.PostCode = reader.GetString(5);
                        c.Town = reader.GetString(6);

                        list.Add(c);
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("błąd GetAllNaczepy: " + query + "\n \n" + e);
                Debug.WriteLine("błąd GetCustomer: " + query + "\n \n" + e);
            }

            return list;
        }
        
        public bool AddEditCustomer(Customer cu)
        {
            bool isAdded = false;
            String query = "exec AddEditCustomer " + cu.Id + ", '" + cu.Name + "', '" + cu.Surname + "', '" + cu.Tel + "', '" + cu.Street + "', '" + cu.PostCode + "', '" + cu.Town + "'";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmdCheck = new SqlCommand(query, conn);
                    SqlDataReader reader = cmdCheck.ExecuteReader();

                    string temp;
                    while (reader.Read())
                    {

                        temp = reader.GetString(1).Trim();

                        if (temp == "") // jeśli rekord został poprawnie dodany/zmieniony to informacja zwrotna jest pusta
                            isAdded = true;
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("błąd AddEditCustomer: " + query + "\n \n" + e);
            }
            return isAdded;
        }

        public Status DeleteCustomer(Customer cu)
        {
            Status stat = new Status();
            String query = "exec DeleteCustomer " + cu.Id;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmdCheck = new SqlCommand(query, conn);
                    SqlDataReader reader = cmdCheck.ExecuteReader();


                    while (reader.Read())
                    {
                        stat.Id = (int)reader[0];
                        stat.Info = reader.GetString(1).Trim();

                        if (stat.Id == 1) // jeśli rekord został poprawnie dodany/zmieniony to Id == 1
                            stat.State = true;
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("błąd DeleteCustomer: " + query + "\n \n" + e);
            }
            return stat;
        }
    }
}
