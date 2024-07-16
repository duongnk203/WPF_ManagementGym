using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string ClassName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int TrainerId { get; set; }

    public DateTime Schedule { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<ClassRegistration> ClassRegistrations { get; set; } = new List<ClassRegistration>();

    public virtual Trainer Trainer { get; set; } = null!;
}
