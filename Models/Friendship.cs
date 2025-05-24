namespace LetThereBeVoice.Models
{
    using LetThereBeVoice.Models;

    public class Friendship
    {
        public int Id { get; set; }

        public int RequesterId { get; set; }
        public int ReceiverId { get; set; }

        public string Status { get; set; } // "Pending", "Accepted", "Rejected"

        public DateTime RequestedAt { get; set; }

        public User Requester { get; set; }
        public User Receiver { get; set; }
    }
}
