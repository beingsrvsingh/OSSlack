namespace Temple.Application.Contracts
{
    public class TempleExceptionDto
    {
        public int TempleMasterId { get; set; }
        public DateTime ExceptionDate { get; set; }
        public string Reason { get; set; } = null!;
    }

}
