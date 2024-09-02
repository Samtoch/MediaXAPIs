﻿namespace MediaXAPIs.Data.Models
{
    public class ProductDetail
    {
        public int Id { get; set; } // Primary Key, Identity column
        public string ProductName { get; set; } 
        public string Description { get; set; } 
        public decimal? Ratings { get; set; } // DECIMAL(8, 1), nullable
        public decimal Price { get; set; } // DECIMAL(18, 2)
        public bool Promoted { get; set; } 
        public char DelFlag { get; set; } = 'N'; 
    }
}