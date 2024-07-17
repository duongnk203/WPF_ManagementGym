using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class MemberDetail
{
    public int MemberId { get; set; }

    public int UserId { get; set; }

    public int MembershipId { get; set; }

    public DateTime JoinDate { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<ClassRegistration> ClassRegistrations { get; set; } = new List<ClassRegistration>();

    public virtual Membership Membership { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User User { get; set; } = null!;
}
