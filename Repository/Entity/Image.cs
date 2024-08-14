using System;
using System.Collections.Generic;

namespace Repository.Entity;

public partial class Image
{
    public string Id { get; set; } = null!;

    public string? LinkImage { get; set; }

    public string? ProductId { get; set; }

    public virtual Product? Product { get; set; }
}
