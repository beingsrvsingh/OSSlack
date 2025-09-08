
namespace AstrologerMicroservice.Domain.Entities
{
    public class ConsultationModeMaster
    {
        public int Id { get; set; }
        public string Mode { get; set; } = String.Empty;
        public int DisplayOrder { get; set; }
        public ICollection<AstrologerConsultationMode> AstrologerConsultationModes { get; set; } = new List<AstrologerConsultationMode>();

    }
}