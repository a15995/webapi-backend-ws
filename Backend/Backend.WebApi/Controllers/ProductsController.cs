using System;
using System.Collections.Generic;
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
            new Product(1, "LaCoste", "Poloshirt", (decimal)299.95),
            new Product(2, "Boss", "Pants", (decimal)1299.95),
            new Product(3, "Levis", "T-shirt", (decimal)399.95)
        };

        private Review[] reviews = new Review[]
        {
            new Review(1, 1, 10, "Super fed polohakker der!"),
            new Review(2, 1, 5, "Sidder for stramt, ellers fedt!"),
            new Review(3, 3, 8, "Superfede jeans af høj kvalitet!")
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        public Product GetProduct(int id)
        {
            foreach (Product product in products)
            {
                if (product.Id == id) // Hvis product.Id = 1 findes i uri http://localhost:x/api/products/1
                {
                    return Ok(product); // ...så returnér Ok(produktdetaljer)
                }
            }
            throw new NotFoundException(); // ... ellers returnér NotFound (tom streng).
        }

        /*public IHttpActionResult GetProduct(int id) // Tester om et givent id findes i Product-arrayet og returnerer product-detaljer (Ok) i Postman.
        {
            foreach (Product product in products)
            {
                if (product.Id == id) // Hvis product.Id = 1 findes i uri http://localhost:x/api/products/1
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
            return reviews.Where(r => r.ProductId == productId); // Returnér reviews, hvor 

            /* Den lange vej:
            var results = new List<Review>();
            foreach (Review review in reviews)
            {
                if (review.ProductId == productId)
                {
                    results.Add(review);
                }
            }
            return results;*/
        }
    }
}
