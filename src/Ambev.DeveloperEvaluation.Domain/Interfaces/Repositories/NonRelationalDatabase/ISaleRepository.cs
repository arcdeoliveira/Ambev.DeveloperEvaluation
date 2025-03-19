using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.Common;

namespace Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.NonRelationalDatabase
{
    public interface ISaleRepository : IBaseMongoDBRepository<Sale>
    {
    }
}
