namespace LetThereBeVoice.Models.ViewModels
{
    public class ChannelMessageCountViewModel
    {
        public int ChannelID { get; set; }
        public string ChannelName { get; set; }
        public int MessageCount { get; set; }
        public int ServerMemberCount { get; set; }
        public double AverageMessageLength { get; set; }
        public DateTime? LastMessageTime { get; set; }



    }
}

