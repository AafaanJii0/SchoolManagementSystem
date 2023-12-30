using IMobi.School.DomainModal.BaseDM;
using System.ComponentModel.DataAnnotations;

namespace IMobi.School.DomainModal.v1.Book
{
    public class AuthorDM : BaseDM<int>
    {
        [StringLength(100), Required]
        public string Name { get; set; }
        [StringLength(300)]
        public string Address { get; set; }
        public virtual HashSet<BookDM> Books { get; set; }
    }
}
