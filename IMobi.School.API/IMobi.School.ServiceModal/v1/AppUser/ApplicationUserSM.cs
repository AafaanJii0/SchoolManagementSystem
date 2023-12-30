using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMobi.School.ServiceModal.v1.AppUser
{
    public class ApplicationUserSM : IdentityUser
    {
        public string FullName { get; set; }
    }
}
