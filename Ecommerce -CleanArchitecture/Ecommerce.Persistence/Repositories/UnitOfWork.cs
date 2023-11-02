using Ecommerce.Application.Interface.Persistence;
using Ecommerce.Persistence.Context;

namespace Ecommerce.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICustomersRepository Customers { get; }
        public IUserRepository Users { get; }
        public ICategoriesRepository Categories { get; }

        public IDiscountRepository Discounts { get; }

        private readonly ApplicationDbContext _applicationDbContext;

        public UnitOfWork(ICustomersRepository customers,
            IUserRepository users,
            ICategoriesRepository categories,
            IDiscountRepository discount,
            ApplicationDbContext dbContext)
        {
            Customers = customers;
            Users = users;
            Categories = categories;
            Discounts = discount;
            _applicationDbContext = dbContext;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<int> Save(CancellationToken cancellationToken)
        {
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
