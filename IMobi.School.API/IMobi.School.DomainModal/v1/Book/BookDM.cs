using IMobi.School.DomainModal.BaseDM;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMobi.School.DomainModal.v1.Book
{
    public class BookDM : BaseDM<int>
    {
        [StringLength(100), Required]
        public string Title { get; set; }
        [Required]
        public DateTime PublishedDate { get; set; }
        public decimal Price { get; set; }
        [StringLength(200)]
        public string Publisher { get; set; }

        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }
        public AuthorDM Author { get; set; }
    }
}