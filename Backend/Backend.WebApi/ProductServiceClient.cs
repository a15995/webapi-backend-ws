/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Backend.WebApi
{
    public class ProductServiceClient // Modul #7, slide 55-56
    {
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://deathstarwebapi.azurewebsites.net/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/products/1");
                if (response.IsSuccessStatusCode)
                {
                    Product product = await response.Content.ReadAsAsync<Product>();
                    // return product; // FEJL som skal rettes - return skal indeholde en liste!
                }
                return null;
            }
        }
        public async Task<Product> GetProduct(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://deathstarwebapi.azurewebsites.net/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/products/" + id);
                if (response.IsSuccessStatusCode)
                {
                    Product product = await response.Content.ReadAsAsync<Product>();
                    return product;
                }
                return null;
            }
        }
    }
}*/