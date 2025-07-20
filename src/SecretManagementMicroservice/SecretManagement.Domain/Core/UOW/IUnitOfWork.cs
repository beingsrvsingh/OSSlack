using SecretManagement.Domain.Core.Repository;
using Shared.Domain.UOW;

namespace SecretManagement.Domain.Core.UOW
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        ISecretRepository SecretRepository { get; }
    }
}
