using System;
using System.Collections.Generic;

namespace Repository.Entity;

public partial class OrderDetail
{
    public string Id { get; set; } = null!;

    public string? ProductId { get; set; }

    public string? OrderId { get; set; }

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
