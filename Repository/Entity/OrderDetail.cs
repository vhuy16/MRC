using System;
using System.Collections.Generic;

namespace Repository.Entity;

public partial class OrderDetail
{
    public Guid Id { get; set; }

    public Guid? ProductId { get; set; }

    public Guid? OrderId { get; set; }

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public DateTime? InsDate { get; set; }

    public DateTime? UpDate { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
