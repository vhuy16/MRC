using System;
using System.Collections.Generic;

namespace Repository.Entity;

public partial class Category
{
    public Guid Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public DateTime? InsDate { get; set; }

    public DateTime? UpDate { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
