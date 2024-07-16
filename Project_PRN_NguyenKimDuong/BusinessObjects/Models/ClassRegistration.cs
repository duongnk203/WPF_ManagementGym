using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class ClassRegistration
{
    public int RegistrationId { get; set; }

    public int ClassId { get; set; }

    public int MemberId { get; set; }

    public DateTime RegistrationDate { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual MemberDetail Member { get; set; } = null!;
}
