using System;
using System.Collections.Generic;

namespace Repository.Entity;

public partial class Product
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Quantity { get; set; }

    public string Status { get; set; } = null!;

    public int CategoryId { get; set; }

    public int ImageId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
