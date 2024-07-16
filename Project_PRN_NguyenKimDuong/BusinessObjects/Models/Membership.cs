using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Membership
{
    public int MembershipId { get; set; }

    public string MembershipType { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<MemberDetail> MemberDetails { get; set; } = new List<MemberDetail>();
}
