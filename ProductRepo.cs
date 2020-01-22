using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace CRUD_Operations
{
    class ProductRepo
    {
        public static string connString;

        public List<Product> GetProducts()
        {
            MySqlConnection conn = new MySqlConnection(connString);
            using(conn)
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "SELECT ProductID, Name, Price, StockLevel FROM products;";
                MySqlDataReader reader = cmd.ExecuteReader();

                var products = new List<Product>();
                while (reader.Read())
                {
                    var row = new Product();
                    row.ProductID = reader.GetInt32("ProductID");
                    row.Name = reader.GetString("Name");
                    row.Price = reader.GetDecimal("Price");
                    row.StockLevel = reader.GetInt32("StockLevel");

                    products.Add(row);

                }
                return products;
                    
                    
            }


        }

        public void UpdateProduct(Product p)
        {
            MySqlConnection conn = new MySqlConnection(connString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE products SET StockLevel=@stock WHERE ProductID=@prodID;";
                cmd.Parameters.AddWithValue("stock", p.StockLevel);
                cmd.Parameters.AddWithValue("prodID", p.ProductID);
                

                cmd.ExecuteNonQuery();
            }
        }
        public void CreateProduct(Product p)
        {
            MySqlConnection conn = new MySqlConnection(connString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO products(Name, Price, CategoryID, OnSale) " +
                    "VALUES(@name, @price, @catID, @sale);";
                cmd.Parameters.AddWithValue("name", p.Name);
                cmd.Parameters.AddWithValue("price", p.Price);
                cmd.Parameters.AddWithValue("catID", p.CategoryID);
                cmd.Parameters.AddWithValue("sale", p.OnSale);

                cmd.ExecuteNonQuery();

            }

        }

        public void DeleteProduct(int productID)
        {
            MySqlConnection conn = new MySqlConnection(connString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM products WHERE ProductID=@prodID;";
                cmd.Parameters.AddWithValue("prodID", productID);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProduct(string name)
        {
            MySqlConnection conn = new MySqlConnection(connString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM products WHERE Name=@name;";
                cmd.Parameters.AddWithValue("name", name);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProduct(string name, int productID)
        {
            MySqlConnection conn = new MySqlConnection(connString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM products WHERE Name=@name AND ProductID=@prodID;";
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("prodID", productID);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
