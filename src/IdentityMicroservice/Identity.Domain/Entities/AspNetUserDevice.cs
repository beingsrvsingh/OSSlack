namespace Identity.Domain.Entities;

public partial class AspNetUserDevice
{
    public int Id { get; set; }

    public required string UserId { get; set; }

    public required string IpAddress { get; set; }

    public required string DeviceName { get; set; }
    public required string Browser { get; set; }
    public required string OS { get; set; }
}
