using IMobi.School.ServiceModal.v1.AppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMobi.School.ServiceModal.v1.Token
{
    public class TokenResponseSM
    {

        public string? AccessToken { get; set; }
        public ClientUserSM LoginUserDetails { get; set; }
        public DateTime ExpiresUtc { get; set; }
    }   
}
