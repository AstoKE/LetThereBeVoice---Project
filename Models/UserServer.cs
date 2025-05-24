using System;

namespace LetThereBeVoice.Models
{
    public class UserServer
    {
        public int UserID { get; set; }
        public User User { get; set; }

        public int ServerID { get; set; }
        public Server Server { get; set; }
        public DateTime JoinDate { get; set; } = DateTime.UtcNow;
    }
}
