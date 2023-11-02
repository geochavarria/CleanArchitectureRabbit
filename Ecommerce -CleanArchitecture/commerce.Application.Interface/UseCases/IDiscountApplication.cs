using Ecommerce.Application.DTO;
using Ecommerce.Transversal.Common;

namespace Ecommerce.Application.Interface.UseCases
{
    public interface IDiscountApplication
    {
        Task<Response<bool>> Create(DiscountDTO discountDTO, CancellationToken cancellationToken = default);
        Task<Response<bool>> Update(DiscountDTO discountDTO, CancellationToken cancellationToken = default);
        Task<Response<bool>> Delete(int id, CancellationToken cancellationToken = default);
        Task<Response<DiscountDTO>> Get(int id, CancellationToken cancellationToken = default);
        Task<Response<IEnumerable<DiscountDTO>>> GetAll(CancellationToken cancellationToken = default);

        Task<ResponsePagination<IEnumerable<DiscountDTO>>> GetAllWithPagination(int pageNumber, int pageSize);
    }


}

