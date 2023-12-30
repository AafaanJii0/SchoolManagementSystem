using IMobi.School.DomainModal.BaseDM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMobi.School.DomainModal.v1
{
    public class StudentDM : BaseDM<int>
    {
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(15)]
        public string Gender { get; set; }
    }
}
