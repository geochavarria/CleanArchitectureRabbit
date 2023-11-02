using Ecommerce.Application.DTO;
using Ecommerce.Transversal.Common;

namespace Ecommerce.Application.Interface.UseCases
{
    public interface ICustomersApplication
    {

        Response<bool> Insert(CustomerDto customer);
        Response<bool> Update(CustomerDto customer);
        Response<bool> Delete(string customerId);
        Response<CustomerDto> Get(string customerId);
        Response<IEnumerable<CustomerDto>> GetAll();
        ResponsePagination<IEnumerable<CustomerDto>> GetAllWithPagination(int pageNumber, int pageSize);

        #region Async
        Task<Response<bool>> InsertAsync(CustomerDto customer);
        Task<Response<bool>> UpdateAsync(CustomerDto customer);
        Task<Response<bool>> DeleteAsync(string customerId);
        Task<Response<CustomerDto>> GetAsync(string customerId);
        Task<Response<IEnumerable<CustomerDto>>> GetAllAsync();
        Task<ResponsePagination<IEnumerable<CustomerDto>>> GetAllWithPaginationAsync(int pageNumber, int pageSize);
        #endregion
    }
}
