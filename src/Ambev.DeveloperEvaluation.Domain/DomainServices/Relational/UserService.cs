using Ambev.DeveloperEvaluation.Domain.DomainServices.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.RelationalDatabase;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.Relational;

namespace Ambev.DeveloperEvaluation.Domain.DomainServices.Relational
{
    public class UserService : BaseRelationalService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;   
        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;   
        }

        public async Task<string?> GetUserNameAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUsernameByIdAsync(userId, cancellationToken);   
        }

        public async Task<bool> UserIsRoleAdminAsync(Guid userId, CancellationToken cancellationToken)
        {
            var userRole = await _userRepository.GetUserRoleAsync(userId, cancellationToken);

            return userRole == UserRole.Admin;
        }
    }
}
