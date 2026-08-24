using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class HomeworkSubmission
{
    public int SubmissionId { get; set; }

    public int HomeworkId { get; set; }

    public int StudentId { get; set; }

    public string? SubmissionText { get; set; }

    public string? FilePath { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public string? Status { get; set; }

    public int? Marks { get; set; }

    public string? Feedback { get; set; }

    public virtual Homework Homework { get; set; } = null!;

    public virtual User Student { get; set; } = null!;
}
