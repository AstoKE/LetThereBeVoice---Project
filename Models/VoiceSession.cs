namespace LetThereBeVoice.Models
{
    public class VoiceSession
    {
        public int VoiceSessionID { get; set; }

        public int UserID { get; set; }
        public int ChannelID { get; set; }

        public DateTime JoinedAt { get; set; }

        // Navigation
        public User User { get; set; }
        public Channel Channel { get; set; }
    }
}
