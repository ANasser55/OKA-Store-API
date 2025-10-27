using System;
using System.Collections.Generic;

namespace OKA.Domain.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool IsAdmin { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
