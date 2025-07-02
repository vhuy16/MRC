using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class CartItem
{
    public Guid Id { get; set; }

    public Guid CartId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime? InsDate { get; set; }

    public DateTime? UpDate { get; set; }

    public string? Status { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
