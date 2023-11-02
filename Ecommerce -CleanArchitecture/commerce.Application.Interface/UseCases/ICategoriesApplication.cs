using Ecommerce.Application.DTO;
using Ecommerce.Transversal.Common;

namespace Ecommerce.Application.Interface.UseCases
{
    public interface ICategoriesApplication
    {
        Task<Response<IEnumerable<CategoryDTO>>> GetAll();
    }
}
