using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class SubjectDTO
    {
        public int SubjectId { get; set; }

        [Required]
        public string SubjectName { get; set; }

        [Required]
        public int ClassId { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
