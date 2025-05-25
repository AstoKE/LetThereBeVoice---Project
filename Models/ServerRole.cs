namespace LetThereBeVoice.Models
{
    public class ServerRole
    {
        public int ServerRoleID { get; set; }

        public int ServerID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }

        public Server Server { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
