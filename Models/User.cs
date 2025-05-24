using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LetThereBeVoice.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = "Active"; // ✅ default value

        public ICollection<Message> Messages { get; set; } = new List<Message>(); // ✅ default to empty
        public ICollection<Server> CreatedServers { get; set; } = new List<Server>(); // ✅ default to empty
    }
}
