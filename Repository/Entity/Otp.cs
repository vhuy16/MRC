using System;
using System.Collections.Generic;

namespace Repository.Entity;

public partial class Otp
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string OtpCode { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public DateTime ExpiresAt { get; set; }

    public bool IsValid { get; set; }

    public virtual User User { get; set; } = null!;
}
