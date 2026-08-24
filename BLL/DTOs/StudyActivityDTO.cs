using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class StudyActivityDTO
    {
        public int ActivityId { get; set; }

        public int StudentId { get; set; }

        public string ActivityType { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime? ActivityDate { get; set; }
    }
}
