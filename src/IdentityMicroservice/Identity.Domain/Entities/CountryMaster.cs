namespace Identity.Domain.Entities;

public partial class CountryMaster
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string AlphaTwoCode { get; set; } = null!;

    public string Emoji { get; set; } = null!;

    public string Unicode { get; set; } = null!;

    public string DialCode { get; set; } = null!;

    public string ImageUri { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<StateMaster> StateMasters { get; set; } = new List<StateMaster>();
}
