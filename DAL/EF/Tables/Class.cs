using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Class
{
    public int ClassId { get; set; }

    public string ClassName { get; set; } = null!;

    public string? Section { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<AttendanceSession> AttendanceSessions { get; set; } = new List<AttendanceSession>();

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual ICollection<Homework> Homeworks { get; set; } = new List<Homework>();

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();

    public virtual ICollection<TeacherAssignment> TeacherAssignments { get; set; } = new List<TeacherAssignment>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
