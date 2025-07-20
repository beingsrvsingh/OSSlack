using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecretManagement.Domain.Entities;

[Table("secrets")]
public partial class Secret
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id", TypeName = "int")]
    public int Id { get; set; }

    [Required, Column("user_id", TypeName = "nvarchar(450)")]
    public required string UserId { get; set; }

    [Required]
    [Column("app_name")]
    public string AppName { get; set; } = null!;

    [Required]
    [Column("environment", TypeName = "varchar(50)")]
    public string Environment { get; set; } = null!;

    [Required]
    [Column("secret_key", TypeName = "varchar(255)")]
    public string SecretKey { get; set; } = null!;

    [Required]
    [Column("secret_value", TypeName = "nvarchar(1000)")]
    public string SecretValue { get; set; } = null!;

    [Required]
    [Column("created_by")]
    public string CreatedBy { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_by")]
    public string? UpdatedBy { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Required]
    [Column("version", TypeName = "int")]
    public int Version { get; set; } = 1;

    [Column("expiry_date")]
    public DateTime? ExpiryDate { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("description", TypeName = "nvarchar(100)")]
    public string? Description { get; set; }
}
