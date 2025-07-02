using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Product
{
    public Guid Id { get; set; }

    public string ProductName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? Message { get; set; }

    public int Quantity { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? InsDate { get; set; }

    public DateTime? UpDate { get; set; }

    public decimal? Price { get; set; }

    public Guid? SubCategoryId { get; set; }

    public DateTime? DelDate { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual SubCategory? SubCategory { get; set; }
}
