    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Hosting.Server;

    namespace LetThereBeVoice.Models
    {
        public class User
        {
            public int UserID { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public DateTime RegistrationDate { get; set; }
            public string Status { get; set; }

            public ICollection<Message> Messages { get; set; }
            public ICollection<Server> CreatedServers { get; set; }
        }
    }

