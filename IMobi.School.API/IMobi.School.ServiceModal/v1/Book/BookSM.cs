using IMobi.School.ServiceModal.BaseSM;

namespace IMobi.School.ServiceModal.v1.Book
{
    public class BookSM : BaseSM<int>
    {
        public string Title { get; set; }
        public DateTime PublishedDate { get; set; }
        public decimal Price { get; set; }
        public string Publisher { get; set; }
        public int AuthorId { get; set; }
    }
}