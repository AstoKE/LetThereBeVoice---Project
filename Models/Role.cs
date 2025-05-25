namespace LetThereBeVoice.Models
{
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; } // e.g. Admin, Member, etc.

        public ICollection<ServerRole> ServerRoles { get; set; }
    }
}
