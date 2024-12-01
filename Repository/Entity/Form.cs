using System;
using System.Collections.Generic;

namespace Repository.Entity;

public partial class Form
{
    public Guid Id { get; set; }

    public string CompanyName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string ServiceType { get; set; } = null!;

    public string Question { get; set; } = null!;

    public DateOnly DateSent { get; set; }
}
