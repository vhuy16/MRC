using System;
using System.Collections.Generic;

namespace Repository.Entity;

public partial class Order
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public Guid PaymentId { get; set; }

    public decimal? TotalPrice { get; set; }

    public string? Status { get; set; }

    public DateTime? InsDate { get; set; }

    public DateTime? UpDate { get; set; }

    public int? ShipCost { get; set; }

    public int? ShipStatus { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Payment Payment { get; set; } = null!;

    public virtual User? User { get; set; }
}
