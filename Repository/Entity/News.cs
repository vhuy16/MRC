using System;
using System.Collections.Generic;

namespace Repository.Entity;

public partial class News
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string SourceUrl { get; set; } = null!;

    public DateTime DatePublished { get; set; }

    public string SourceName { get; set; } = null!;

    public string Status { get; set; } = null!;
}
