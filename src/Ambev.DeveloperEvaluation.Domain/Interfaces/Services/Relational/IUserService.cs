using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.Common;

namespace Ambev.DeveloperEvaluation.Domain.Interfaces.Services.Relational
{
    public interface IUserService : IBaseRelationalService<User, Guid>
    {
        Task<bool> UserIsRoleAdminAsync(Guid userId, CancellationToken cancellationToken);
        Task<string?> GetUserNameAsync(Guid userId, CancellationToken cancellationToken);
    }

}
