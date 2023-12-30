using IMobi.School.ServiceModal.EnumSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMobi.School.ServiceModal.v1.Token
{
    public class TokenRequestSM
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public RoleTypeSM RoleTypeSM { get; set; }
    }
}
