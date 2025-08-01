using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;

[Table("users")]
public partial class User
{
    public int Id { get; set; }

    public int BranchId { get; set; }

    public string Username { get; set; } = null!;
    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Role { get; set; }

    public DateTime? CreatedOn { get; set; }
}
