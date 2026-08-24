using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string? Author { get; set; }

    public int ClassId { get; set; }

    public int SubjectId { get; set; }

    public string? FilePath { get; set; }

    public int UploadedBy { get; set; }

    public DateTime? UploadedAt { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;

    public virtual User UploadedByNavigation { get; set; } = null!;
}
