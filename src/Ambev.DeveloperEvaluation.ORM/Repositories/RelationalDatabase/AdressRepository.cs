using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.RelationalDatabase;
using Ambev.DeveloperEvaluation.ORM.Contexts;
using Ambev.DeveloperEvaluation.ORM.Repositories.Common;

namespace Ambev.DeveloperEvaluation.ORM.Repositories.RelationalDatabase
{
    public class AdressRepository : BaseRelationalDatabaseRepository<Adress>, IAdressRepository
    {
        protected AdressRepository(PostgreContext context) : base(context)
        {
        }
    }
}
