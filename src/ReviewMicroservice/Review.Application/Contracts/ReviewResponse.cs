namespace Review.Application.Contracts
{
    public class ReviewResponse
    {
        public required string Id { get; set; }

        public required string ProductId { get; set; }

        public required string UserName { get; set; }

        public int Star { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsHelpFulMarked { get; set; }

        public bool IsReported { get; set; }

        ~ReviewResponse()
        {
            Console.WriteLine("Destructor - ");
        }
    }
}
