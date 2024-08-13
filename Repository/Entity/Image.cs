using System;
using System.Collections.Generic;

namespace Repository.Entity;

public partial class Image
{
    public int Id { get; set; }

    public string? LinkImage { get; set; }

    public int? ProductId { get; set; }

    public virtual Product? Product { get; set; }
}
