using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Form
{
    public Guid Id { get; set; }

    public string CompanyName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string ServiceType { get; set; } = null!;

    public string? Question { get; set; }

    public DateTime? DateSent { get; set; }
}
