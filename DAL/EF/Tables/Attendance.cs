using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int SessionId { get; set; }

    public int StudentId { get; set; }

    public string Status { get; set; } = null!;

    public string? Remarks { get; set; }

    public virtual AttendanceSession Session { get; set; } = null!;

    public virtual User Student { get; set; } = null!;
}
