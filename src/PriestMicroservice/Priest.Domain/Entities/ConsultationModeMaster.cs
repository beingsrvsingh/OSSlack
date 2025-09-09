
namespace PriestMicroservice.Domain.Entities
{
    public class ConsultationModeMaster
    {
        public int Id { get; set; }
        public string Mode { get; set; } = String.Empty;
        public int DisplayOrder { get; set; }
        public ICollection<ConsultationMode> ConsultationModes { get; set; } = new List<ConsultationMode>();

    }
}