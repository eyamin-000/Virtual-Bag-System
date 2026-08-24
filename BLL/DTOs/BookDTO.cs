using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class BookDTO
    {
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; } = null!;

        public string? Author { get; set; }

        public int ClassId { get; set; }

        public int SubjectId { get; set; }

        public string? FilePath { get; set; }

        public int UploadedBy { get; set; }

        public DateTime? UploadedAt { get; set; }
    }
}
