using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class TeacherAssignmentDTO
    {
        public int AssignmentId { get; set; }

        public int TeacherId { get; set; }

        public int ClassId { get; set; }

        public int SubjectId { get; set; }

        public DateTime? AssignedAt { get; set; }
    }
}
