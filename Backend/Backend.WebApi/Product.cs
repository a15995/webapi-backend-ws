using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.WebApi
{
    public class Product
    {
        private int id; // Felt
        private string name;
        private string category;
        private decimal price;

        public int Id // Property
        {
            get
            {
                return id;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
        }
        public string Category
        {
            get
            {
                return category;
            }
        }
        public decimal Price
        {
            get
            {
                return price;
            }
        }

        public Product(int id, string name, string category, decimal price) // Constructor
        {
            this.id = id;
            this.name = name;
            this.category = category;
            this.price = price;
        }
    }
}