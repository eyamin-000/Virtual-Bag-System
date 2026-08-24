using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class ClassDTO
    {
        public int ClassId { get; set; }

        [Required]
        public string ClassName { get; set; }

        public string? Section { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
