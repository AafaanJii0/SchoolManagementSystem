using IMobi.School.ServiceModal.BaseSM;

namespace IMobi.School.ServiceModal.v1.Book
{
    public class AuthorSM : BaseSM<int>
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
