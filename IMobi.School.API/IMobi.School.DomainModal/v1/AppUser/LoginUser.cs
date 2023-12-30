using IMobi.School.DomainModal.EnumDM;

namespace IMobi.School.DomainModal.v1.AppUser
{
    public class LoginUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public RoleTypeDM RoleType { get; set; }
    }
}
