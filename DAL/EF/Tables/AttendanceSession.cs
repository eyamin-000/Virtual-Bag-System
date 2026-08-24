using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class AttendanceSession
{
    public int SessionId { get; set; }

    public int TeacherId { get; set; }

    public int ClassId { get; set; }

    public int SubjectId { get; set; }

    public DateOnly AttendanceDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Class Class { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;

    public virtual User Teacher { get; set; } = null!;
}
