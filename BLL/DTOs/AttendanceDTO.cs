using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class AttendanceDTO
    {
        public int AttendanceId { get; set; }

        public int SessionId { get; set; }

        public int StudentId { get; set; }

        public string Status { get; set; } = null!;

        public string? Remarks { get; set; }
    }
}
