using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interface.Persistence
{
    public interface ICustomersRepository : IGenericRepository<Customer>
    {
        //bool Insert(Customers customer);
        //bool Update(Customers customer);
        //bool Delete(string customerId);

        //Customers Get(string customerId);
        //IEnumerable<Customers> GetAll();


        //#region Async
        //Task<bool> InsertAsync(Customers customer);
        //Task<bool> UpdateAsync(Customers customer);
        //Task<bool> DeleteAsync(string customerId);
        //Task<Customers> GetAsync(string customerId);
        //Task<IEnumerable<Customers>> GetAllAsync();
        //#endregion
    }
}
