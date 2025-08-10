
namespace Temple.Domain.Entities.Enums
{
    [Flags]
    public enum ConsultationMode
    {
        None = 0,
        Chat = 1 << 0,
        Audio = 1 << 1,
        Video = 1 << 2,
        HomeVisit = 1 << 3
    }
}