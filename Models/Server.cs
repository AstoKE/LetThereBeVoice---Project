    using System;
    using System.Collections.Generic;
    using System.Threading.Channels;

    namespace LetThereBeVoice.Models
    {
        public class Server
        {
            public int ServerID { get; set; }
            public string ServerName { get; set; }
            public DateTime CreationDate { get; set; }

            public int CreatorID { get; set; }
            public User Creator { get; set; }

            public ICollection<Channel> Channels { get; set; }
        }
    }


