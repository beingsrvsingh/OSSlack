namespace Identity.Domain.Entities;

public partial class CityMaster
{
    public int Id { get; set; }

    public int StateMasterId { get; set; }

    public int Pincode { get; set; }

    public string DistrictName { get; set; } = null!;

    public string AreaName { get; set; } = null!;

    public bool IsDeliverable { get; set; }

    public bool IsActive { get; set; }

    public virtual StateMaster StateMaster { get; set; } = null!;
}
