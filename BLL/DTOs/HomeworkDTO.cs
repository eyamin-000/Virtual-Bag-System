using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class HomeworkDTO
    {
        public int HomeworkId { get; set; }

        public int TeacherId { get; set; }

        public int ClassId { get; set; }

        public int SubjectId { get; set; }
        [Required]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }
        [Required]
        public DateTime Deadline { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string? DeadlineStatus { get; set; }
    }
}
