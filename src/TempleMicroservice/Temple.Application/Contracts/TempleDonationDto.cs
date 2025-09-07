namespace Temple.Application.Contracts
{
    public class TempleDonationDto
    {
        public int TempleMasterId { get; set; }
        public string DonorName { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime DonationDate { get; set; }
        public string? Notes { get; set; }
    }

}
