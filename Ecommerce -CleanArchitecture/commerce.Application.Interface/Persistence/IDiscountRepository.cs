using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interface.Persistence
{
    public interface IDiscountRepository : IGenericRepository<Discount>
    {

        Task<Discount> GetAsync(string id, CancellationToken cancellationToken);
        Task<IEnumerable<Discount>> GetAllAsync(CancellationToken cancellationToken);
    }
}
