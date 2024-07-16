using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public bool? Status { get; set; }

    public string Address { get; set; } = null!;

    public virtual ICollection<MemberDetail> MemberDetails { get; set; } = new List<MemberDetail>();

    public virtual ICollection<Trainer> Trainers { get; set; } = new List<Trainer>();
}
