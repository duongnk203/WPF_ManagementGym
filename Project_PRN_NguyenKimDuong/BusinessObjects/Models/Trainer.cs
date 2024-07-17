using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Trainer
{
    public int TrainerId { get; set; }

    public int UserId { get; set; }

    public string Specialization { get; set; } = null!;

    public int ExperienceYears { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual User User { get; set; } = null!;
}
