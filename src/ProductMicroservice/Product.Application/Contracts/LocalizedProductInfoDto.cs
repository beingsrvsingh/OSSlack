
namespace Product.Application.Contracts
{
    public class LocalizedProductInfoDto
    {
        public int Id { get; set; }
        public string Language { get; set; } = null!;
        public string LocalizedName { get; set; } = null!;
        public string LocalizedDescription { get; set; } = null!;
    }

}