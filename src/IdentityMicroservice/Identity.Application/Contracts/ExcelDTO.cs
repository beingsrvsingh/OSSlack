namespace Identity.Application.Contracts
{
    internal class ExcelDTO
    {
        public int StateMasterId { get; set; }
        public string StateName { get; set; } = null!;
        public int Pincode { get; set; }
        public string DistrictName { get; set; } = null!;
        public string AreaName { get; set; } = null!;
    }
}
