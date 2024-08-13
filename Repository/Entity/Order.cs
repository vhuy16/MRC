using System;
using System.Collections.Generic;

namespace Repository.Entity;

public partial class Order
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public decimal? TotalPrice { get; set; }

    public string? Status { get; set; }

    public DateOnly? CreateDate { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User? User { get; set; }
}
