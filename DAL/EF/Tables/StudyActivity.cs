using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class StudyActivity
{
    public int ActivityId { get; set; }

    public int StudentId { get; set; }

    public string ActivityType { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? ActivityDate { get; set; }

    public virtual User Student { get; set; } = null!;
}
