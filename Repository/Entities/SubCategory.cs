using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class SubCategory
{
    public Guid Id { get; set; }

    public Guid CategoryId { get; set; }

    public string SubCategoryName { get; set; } = null!;

    public string? Status { get; set; }

    public DateTime? InsDate { get; set; }

    public DateTime? UpDate { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
