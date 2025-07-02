using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Service
{
    public Guid Id { get; set; }

    public string ServiceName { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime? InsDate { get; set; }

    public DateTime? UpDate { get; set; }

    public DateTime? DeleteAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
