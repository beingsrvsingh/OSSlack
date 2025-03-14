namespace Shared.Domain.Common.Entities.Interface
{
    public interface ISoftDeleteEntity
    {
        public bool IsDeleted { get; set; }
    }
}
