using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class TeacherAssignment
{
    public int AssignmentId { get; set; }

    public int TeacherId { get; set; }

    public int ClassId { get; set; }

    public int SubjectId { get; set; }

    public DateTime? AssignedAt { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;

    public virtual User Teacher { get; set; } = null!;
}
