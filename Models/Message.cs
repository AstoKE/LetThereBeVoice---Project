using System;
namespace LetThereBeVoice.Models
    {
        public class Message
        {
            public int MessageID { get; set; }
            public string Content { get; set; }
            public DateTime SentAt { get; set; }

            public int SenderID { get; set; }
            public User Sender { get; set; }

            public int ChannelID { get; set; }
            public Channel Channel { get; set; }
        }
    }


