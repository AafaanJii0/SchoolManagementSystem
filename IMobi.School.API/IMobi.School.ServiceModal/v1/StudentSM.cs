using IMobi.School.ServiceModal.BaseSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMobi.School.ServiceModal.v1
{
    public class StudentSM : BaseSM<int>
    {
        public string Name { get; set; }
        public string Gender { get; set; }
    }
}
