using System;
using System.Collections.Generic;

namespace kch_backend.Entities;

public partial class user
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Role { get; set; }

    public DateTime? CreatedOn { get; set; }
}
