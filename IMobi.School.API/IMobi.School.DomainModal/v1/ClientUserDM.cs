using IMobi.School.DomainModal.BaseDM;
using IMobi.School.DomainModal.EnumDM;
using System.ComponentModel.DataAnnotations;

namespace IMobi.School.DomainModal.v1
{
    public class ClientUserDM : BaseDM<int>//: IdentityUser<int>
    {
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public RoleTypeDM RoleTypeDM { get; set; }
    }
}
