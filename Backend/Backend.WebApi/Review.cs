using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.WebApi
{
    public class Review
    {
        public int Id { get; set; } // Property
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
    }
}