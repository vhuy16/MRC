using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Order
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public decimal? TotalPrice { get; set; }

    public string? Status { get; set; }

    public DateTime? InsDate { get; set; }

    public DateTime? UpDate { get; set; }

    public int? ShipStatus { get; set; }

    public int? ShipCost { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User? User { get; set; }
}
