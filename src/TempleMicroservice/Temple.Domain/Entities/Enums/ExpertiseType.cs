namespace Temple.Domain.Entities.Enums
{
    [Flags]
    public enum ExpertiseType
    {
        None = 0,
        Kundli = 1 << 0,
        Pooja = 1 << 1,
        Consultation = 1 << 2,
        Matchmaking = 1 << 3,
        MindReading = 1 << 4,
        // Add more
    }
}