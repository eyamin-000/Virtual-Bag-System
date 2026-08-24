using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Note
{
    public int NoteId { get; set; }

    public int StudentId { get; set; }

    public int SubjectId { get; set; }

    public string Title { get; set; } = null!;

    public string? NoteContent { get; set; }

    public string? NoteColor { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual User Student { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
