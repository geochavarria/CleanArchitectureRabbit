using Dapper;
using Ecommerce.Application.Interface.Persistence;
using Ecommerce.Domain.Entities;
using Ecommerce.Persistence.Context;

namespace Ecommerce.Persistence.Repositories
{
    public class UsersRepository : IUserRepository
    {
        //private readonly IConnectionFactory _connectionFactory;
        private readonly DapperContext _context;
        public UsersRepository(DapperContext context)
        {
            //IConnectionFactory connectionFactory
            //) {
            //_connectionFactory =  connectionFactory;
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            using var dbConnection = _context.CreateConnection();
            var query = "UsersGetByUserAndPassword";
            var parameters = new DynamicParameters();
            parameters.Add("username", username);
            parameters.Add("password", password);

            var user = dbConnection.QuerySingle<User>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            return user!;

        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public User Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllWithPagination(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
