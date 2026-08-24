using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class HomeworkSubmissionDTO
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
    }
}
