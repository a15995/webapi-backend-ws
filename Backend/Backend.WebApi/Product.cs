using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.WebApi
{
    public class Product : TableEntity
    {
        //public int Id { get; set; } // Property
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; } // Ændret fra decimal til double
    }
}