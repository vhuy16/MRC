using System;
using System.Collections.Generic;

namespace Repository.Entity;

public partial class Category
{
    public string Id { get; set; } = null!;

    public string? CategoryName { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
