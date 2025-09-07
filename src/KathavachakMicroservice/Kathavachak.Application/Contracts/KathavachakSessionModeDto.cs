namespace Kathavachak.Application.Contracts
{
    public class KathavachakSessionModeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
