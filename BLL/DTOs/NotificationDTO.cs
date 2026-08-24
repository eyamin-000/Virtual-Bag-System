using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class NotificationDTO
    {
        public int NotificationId { get; set; }

        public int UserId { get; set; }

        public string Message { get; set; } = null!;

        public bool? IsRead { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
