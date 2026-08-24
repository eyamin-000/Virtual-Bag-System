using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class NoteDTO
    {
        public int NoteId { get; set; }

        public int StudentId { get; set; }

        public int SubjectId { get; set; }

        public string Title { get; set; } = null!;

        public string? NoteContent { get; set; }

        public string? NoteColor { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
