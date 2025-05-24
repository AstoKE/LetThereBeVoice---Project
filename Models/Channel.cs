    using System;
    using System.Collections.Generic;

    namespace LetThereBeVoice.Models
    {
        public class Channel
        {
            public int ChannelID { get; set; }
            public string ChannelName { get; set; }
            public string ChannelType { get; set; } // 'Text' or 'Voice'
            public DateTime CreatedDate { get; set; }

            public int ServerID { get; set; }
            public Server Server { get; set; }

            public ICollection<Message> Messages { get; set; }
        }
    }

