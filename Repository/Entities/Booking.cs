using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Booking
{
    public Guid Id { get; set; }

    public Guid? ServiceId { get; set; }

    public DateTime? BookingDate { get; set; }

    public string? Status { get; set; }

    public DateTime? InsDate { get; set; }

    public DateTime? UpDate { get; set; }

    public string Content { get; set; } = null!;

    public string? Title { get; set; }

    public virtual Service? Service { get; set; }
}
