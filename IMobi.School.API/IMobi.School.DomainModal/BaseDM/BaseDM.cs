using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IMobi.School.DomainModal.BaseDM
{
    public class BaseDM<T> where T : struct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
        [StringLength(100)]
        [JsonPropertyName("createdBy")]
        public string? CreatedBy { get; set; }
        [StringLength(100)]
        [JsonPropertyName("modifiedBy")]
        public string? ModifiedBy { get; set; }
        public DateTime? CreatedDateUTC { get; set; }
        public DateTime? ModifiedDateUTC { get; set; }
    }
}
