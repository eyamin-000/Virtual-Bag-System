using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string SubjectName { get; set; } = null!;

    public int ClassId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<AttendanceSession> AttendanceSessions { get; set; } = new List<AttendanceSession>();

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual Class Class { get; set; } = null!;

    public virtual ICollection<Homework> Homeworks { get; set; } = new List<Homework>();

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public virtual ICollection<TeacherAssignment> TeacherAssignments { get; set; } = new List<TeacherAssignment>();
}
