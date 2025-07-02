using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Repository.Entities;

public partial class News
{
    [Key]
    public Guid Id { get; set; }

    public string Content { get; set; } = null!;

    public string Type { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? InsDate { get; set; }

    public DateTime? UpDate { get; set; }

    public DateTime? DelDate { get; set; }
}
