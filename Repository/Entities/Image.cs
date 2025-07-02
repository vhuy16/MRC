using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Image
{
    public Guid Id { get; set; }

    public string? LinkImage { get; set; }

    public Guid? ProductId { get; set; }

    public DateTime? InsDate { get; set; }

    public DateTime? UpDate { get; set; }

    public virtual Product? Product { get; set; }
}
