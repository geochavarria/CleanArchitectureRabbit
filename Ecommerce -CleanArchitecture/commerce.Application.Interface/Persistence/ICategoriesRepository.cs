using Ecommerce.Domain.Entities;


namespace Ecommerce.Application.Interface.Persistence
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Category>> GetAll();
    }
}
