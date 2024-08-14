using System;
using System.Collections.Generic;

namespace Repository.Entity;

public partial class Order
{
    public string Id { get; set; } = null!;

    public string? UserId { get; set; }

    public decimal? TotalPrice { get; set; }

    public string? Status { get; set; }

    public DateOnly? CreateDate { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User? User { get; set; }
}
