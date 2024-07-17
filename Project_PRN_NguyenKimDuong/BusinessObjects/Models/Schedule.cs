using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public string ScheduleName { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
