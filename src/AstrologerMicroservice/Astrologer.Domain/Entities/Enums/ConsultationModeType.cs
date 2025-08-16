
namespace AstrologerMicroservice.Domain.Entities.Enums
{
    [Flags]
    public enum ConsultationModeType
    {
        None = 0,
        Online = 1 << 0,
        InPerson = 1 << 1,
        Phone = 1 << 2,
        VideoCall = 1 << 3,
        Chat = 1 << 4
    }
}