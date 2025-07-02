using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Cart
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public DateTime? InsDate { get; set; }

    public DateTime? UpDate { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual User User { get; set; } = null!;
}
