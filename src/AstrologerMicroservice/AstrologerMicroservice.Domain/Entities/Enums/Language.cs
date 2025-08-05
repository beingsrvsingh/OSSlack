namespace AstrologerMicroservice.Domain.Entities.Enums
{
    [Flags]
    public enum Languages
    {
        None = 0,
        Hindi = 1 << 0,
        English = 1 << 1,
        Tamil = 1 << 2,
        Bengali = 1 << 3,
        Marathi = 1 << 4,
        // Add more
    }
}