using Dapper;
using Ecommerce.Application.Interface.Persistence;
using Ecommerce.Domain.Entities;
using Ecommerce.Persistence.Context;
using System.Data;

namespace Ecommerce.Persistence.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly DapperContext _context;
        public CategoriesRepository(DapperContext dapper)
        {
            _context = dapper;

        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            using var dbConnection = _context.CreateConnection();
            var query = "SELECT *from Categories";
            var categories = await dbConnection.QueryAsync<Category>(
                query,
                commandType: CommandType.Text);

            return categories!;
        }
    }
}
