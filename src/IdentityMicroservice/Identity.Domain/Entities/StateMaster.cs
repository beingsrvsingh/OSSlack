namespace Identity.Domain.Entities;

public partial class StateMaster
{
    public int Id { get; set; }

    public int CountryMasterId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<CityMaster> CityMasters { get; set; } = new List<CityMaster>();

    public virtual CountryMaster CountryMaster { get; set; } = null!;
}
