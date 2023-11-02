using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interface.Persistence
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User Authenticate(string username, string password);
    }
}
