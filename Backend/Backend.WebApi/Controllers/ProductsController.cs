using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Backend.WebApi.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private Product[] products = new Product[] // Initiér array, der vha. constructor (Product.cs) tilføjer produkter til arrayet (databasen).
        {
            //new Product { Id = 1, Name = "LaCoste", Category = "Poloshirt", Price = 299.95 },
            //new Product { Id = 2, Name = "Boss", Category = "Pants", Price = 1299.95 },
            //new Product { Id = 3, Name = "Levis", Category = "T-shirt", Price = 399.95 }
            new Product { PartitionKey = "ProductKey", RowKey = "0", Name = "LaCoste", Category = "Poloshirt", Price = 299.95 },
            new Product { PartitionKey = "ProductKey", RowKey = "1", Name = "Boss", Category = "Pants", Price = 1299.95 },
            new Product { PartitionKey = "ProductKey", RowKey = "2", Name = "Levis", Category = "T-shirt", Price = 399.95 }
        };

        private Review[] reviews = new Review[] // Initiér array, der vha. constructor (Review.cs) tilføjer anmeldelser til arrayet (databasen).
        {
            new Review { Id = 1, ProductId = 1, Rating = 10, Text = "Super fed polohakker der!" },
            new Review { Id = 2, ProductId = 1, Rating = 5, Text = "Sidder for stramt, ellers fedt!" },
            new Review { Id = 3, ProductId = 3, Rating = 8, Text = "Superfede jeans af høj kvalitet!" }
        };

        [Route("")]
        [HttpGet] // GET-metode
        public IEnumerable<Product> GetAllProducts()
        {
            CloudTableClient tableClient = CreateTableClient(); // Sæt variablen client til tableClient
            CloudTable table = tableClient.GetTableReference("Products");
            var query =
                from entity in table.CreateQuery<Product>()
                where entity.PartitionKey == "ProductKey"
                select entity;

            return query;
            // return products;
        }

        /*public IHttpActionResult GetProduct(int id) // Tester om et givent id findes i Product-arrayet og returnerer product-detaljer (Ok) i Postman.
       {
           foreach (Product product in products)
           {
               if (product.Id == id) // Hvis id = 1 findes i URI http://localhost:x/api/products/1
               {
                   return Ok(product); // ...så returnér Ok(produktdetaljer)
               }
           }
           return NotFound(); // ... ellers returnér NotFound (tom streng).
       }*/

        [Route("{productId}/reviews")] // URI til reviews konstrueres
       [HttpGet] // GET-metode
       public IEnumerable<Review> GetReviewsForProduct(int productId)
       {
           return reviews.Where(r => r.ProductId == productId); // Returnér reviews, hvor reviewets ProductId matcher productId

           /* Den lange vej:
           var results = new List<Review>(); // Generer ny liste med reviews (results), som møder kravene
           foreach (Review review in reviews)
           {
               if (review.ProductId == productId)
               {
                   results.Add(review); // Tilføj til resultatlisten, hvis kravet mødes
               }
           }
           return results; // Returnér resultatlisten*/
        }

        [Route("{id}")] // URI til reviews konstrueres
        [HttpGet] // GET-metode
        public Product GetProduct(string id) // Ændret fra int til string
        {
            /*foreach (Product product in products)
            {
                if (product.Id == id) // Hvis id = 1 findes i URI {RoutePrefix}/{productId}
                {
                    return product; // ...så returnér produktdetaljer
                }
            }
            throw new NotFoundException(); // ... ellers kast ny NotFoundException (returnerer tekst).*/
            CloudTableClient tableClient = CreateTableClient();
            CloudTable table = tableClient.GetTableReference("Products");
            var query =
                from entity in table.CreateQuery<Product>()
                where entity.PartitionKey == "ProductKey" && entity.RowKey == id
                select entity;

            var res = query.FirstOrDefault();
            
            return res;
            
            // Alternativ metode med LINQ og anden result-type

            /*public IHttpActionResult GetProduct(int id)
            {
                var product = this.products.Where(p => p.Id == id)
                    .SingleOrDefault();
                if (product == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(product);
                }
            }*/
        }

        private CloudTableClient CreateTableClient()
        {
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(
                    ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            return tableClient;
        }

        internal void InitializeSampleData()
        {
            CloudTableClient tableClient = CreateTableClient(); // Sæt variablen client til tableClient

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("Products");

            // Slet tabellen hvis den eksisterer.
            //table.DeleteIfExists();

            // Create the table if it doesn't exist.
            table.CreateIfNotExists();

            foreach (Product product in products)
            {
                TableOperation insertOperation = TableOperation.InsertOrReplace(product); // Indsæt eller erstat indholdet i databasen fra lokal liste
                table.Execute(insertOperation); // Udfør operationen
            }
        }
    }
}
