using IMobi.School.DomainModal.EnumDM;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMobi.School.DomainModal.v1.AppUser
{
    public class RoleTypeDM : IdentityRole
    {
        public RoleType RoleType { get; set; }
        
    }
}
