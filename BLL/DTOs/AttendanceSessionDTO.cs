using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class AttendanceSessionDTO
    {
        public int SessionId { get; set; }

        public int TeacherId { get; set; }

        public int ClassId { get; set; }

        public int SubjectId { get; set; }

        public DateOnly AttendanceDate { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
