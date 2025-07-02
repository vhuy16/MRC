using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Payment
{
    public Guid PaymentId { get; set; }

    public Guid UserId { get; set; }

    public decimal Amount { get; set; }

    public string? PaymentMethod { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long? OrderCode { get; set; }

    public Guid? OrderId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual User User { get; set; } = null!;
}
