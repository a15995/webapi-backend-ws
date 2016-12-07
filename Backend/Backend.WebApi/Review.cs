using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.WebApi
{
    public class Review
    {
        private int id; // Felt
        private int productId;
        private int rating;
        private string text;

        public int Id // Property
        {
            get
            {
                return id;
            }
        }
        public int ProductId
        {
            get
            {
                return productId;
            }
        }
        public int Rating
        {
            get
            {
                return rating;
            }
        }
        public string Text
        {
            get
            {
                return text;
            }
        }

        public Review(int id, int productId, int rating, string text) // Constructor
        {
            this.id = id;
            this.productId = productId;
            this.rating = rating;
            this.text = text;
        }
    }
}