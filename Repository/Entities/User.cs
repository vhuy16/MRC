﻿using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public string? FullName { get; set; }

    public string? Status { get; set; }

    public string? Gender { get; set; }

    public string Role { get; set; } = null!;

    public DateTime? InsDate { get; set; }

    public DateTime? UpDate { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? DelDate { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Otp> Otps { get; set; } = new List<Otp>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
