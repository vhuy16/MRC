﻿using Repository.Entity;

namespace MRC_API.Payload.Response.CartItem
{
    public class AddCartItemResponse
    {
        public Guid CartItemId { get; set; } 
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public virtual ICollection<string> Images { get; set; } = new List<string>();
        public string CategoryName { get; set; }
    }
}
