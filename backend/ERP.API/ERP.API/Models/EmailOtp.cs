using System;
using System.ComponentModel.DataAnnotations;

namespace ERP.API.Models
{
    public class EmailOtp
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string OtpHash { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime ExpiresAt { get; set; }

        public bool IsVerified { get; set; } = false;

        public bool IsUsed { get; set; } = false;

        public int Attempts { get; set; } = 0;
    }
}
