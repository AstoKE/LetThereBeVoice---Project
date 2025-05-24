namespace LetThereBeVoice.Models
{
    public class UserRecentActivity
    {
        public string Username { get; set; }
        public string ChannelName { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
    }

}
