namespace IMobi.School.ServiceModal.BaseSM
{
    public class BaseSM<T> where T : struct
    {
        public T Id { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? CreatedDateUTC { get; set; }
        public DateTime? ModifiedDateUTC { get; set; }
    }
}
