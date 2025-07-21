namespace SecretManagement.Domain.Entities;

public class ApiSecret
{
    public int Id { get; set; }

    public required string UserId { get; set; }  // FK to user who created it

    public string Name { get; set; } = null!; // Friendly name

    public string ApiKey { get; set; } = null!; // Store hashed key ideally

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? LastUsedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public string? Description { get; set; }    
}
