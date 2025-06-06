using System;
using System.Collections.Generic;

namespace Repository.Entity;

public partial class News
{
    public Guid Id { get; set; }

    public string Content { get; set; } = null!;

    public string Type { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? InsDate { get; set; }

    public DateTime? UpDate { get; set; }

    public DateTime? DelDate { get; set; }
}
