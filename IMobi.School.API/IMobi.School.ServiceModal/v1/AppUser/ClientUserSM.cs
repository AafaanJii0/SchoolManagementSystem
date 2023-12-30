using IMobi.School.ServiceModal.EnumSM;

namespace IMobi.School.ServiceModal.v1.AppUser
{
    public class ClientUserSM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public RoleTypeSM RoleTypeSM { get; set; }
    }
}
