using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Homework
{
    public int HomeworkId { get; set; }

    public int TeacherId { get; set; }

    public int ClassId { get; set; }

    public int SubjectId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime Deadline { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual ICollection<HomeworkSubmission> HomeworkSubmissions { get; set; } = new List<HomeworkSubmission>();

    public virtual Subject Subject { get; set; } = null!;

    public virtual User Teacher { get; set; } = null!;
}
