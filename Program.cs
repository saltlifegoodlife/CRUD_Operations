using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
namespace CRUD_Operations
{
    class Program
    {
        static void Main(string[] args)
        {
            // setup for hiding your server connection
            string defaultKey = File.ReadAllText("appsettings.Debug.json");
            JObject jObject = JObject.Parse(defaultKey);
            JToken token = jObject["DefaultConnection"];
            string connectionString = token.ToString();
            ProductRepo.connString = connectionString;




            ProductRepo repo = new ProductRepo();

            //Create products
            Console.WriteLine("Creating Product........");
            var newProduct = new Product { Name = "Toss Product", Price = 19.99M, CategoryID = 2, OnSale = 0 };
            repo.CreateProduct(newProduct);
            Console.WriteLine("Product Created!");

            //Update Products
            Console.WriteLine("Updating Product.....");
            var newInfo = new Product { ProductID = 943, StockLevel = 27 };
            repo.UpdateProduct(newInfo);

            //Delete products
            repo.DeleteProduct(943);
            repo.DeleteProduct("Ross Product");
            repo.DeleteProduct("Toss Product", 946);


            //Read Products
            List<Product> products = repo.GetProducts();

            foreach (var prod in products)
            {
                Console.WriteLine($"{prod.ProductID}  {prod.Name} ------  ${prod.Price} ------- stock level is {prod.StockLevel}");
            }

        }
    }
}
