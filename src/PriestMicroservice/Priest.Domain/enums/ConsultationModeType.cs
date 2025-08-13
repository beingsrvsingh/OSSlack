
namespace Priest.Domain.Entities.Enums
{
    [Flags]
    public enum ConsultationModeType
    {
        None = 0,
        Chat = 1 << 0,
        Audio = 1 << 1,
        Video = 1 << 2,
        HomeVisit = 1 << 3,
        Temple = 1 << 4
    }
}