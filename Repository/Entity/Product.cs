using System;
using System.Collections.Generic;

namespace Repository.Entity;

public partial class Product
{
    public string Id { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Quantity { get; set; }

    public string Status { get; set; } = null!;

    public string CategoryId { get; set; } = null!;

    public int ImageId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
