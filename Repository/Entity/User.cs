using System;
using System.Collections.Generic;

namespace Repository.Entity;

public partial class User
{
    public string Id { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public string? FullName { get; set; }

    public string? Status { get; set; }

    public string? Gender { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
