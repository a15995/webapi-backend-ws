using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.WebApi
{
    public class Product : TableEntity
    {
        //private int id; // Felt
        private string name;
        private string category;
        private double price;

        /*public int Id // Property
        {
            get
            {
                return id;
            }
        }*/
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
        public double Price
        {
            get
            {
                return price;
            }
        }

        public Product(string name, string category, double price) // Constructor
        {
            //this.id = id;
            this.name = name;
            this.category = category;
            this.price = price;
        }
    }
}