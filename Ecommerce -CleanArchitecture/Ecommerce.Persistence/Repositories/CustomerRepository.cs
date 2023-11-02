using Dapper;
using Ecommerce.Application.Interface.Persistence;
using Ecommerce.Domain.Entities;
using Ecommerce.Persistence.Context;
using System.Data;

namespace Ecommerce.Persistence.Repositories
{
    public class CustomerRepository : ICustomersRepository
    {
        private readonly DapperContext _context;


        public CustomerRepository(DapperContext context)
        {
            _context = context;
        }

        public bool Insert(Customer customer)
        {
            using var dbConnection = _context.CreateConnection();
            var query = "CustomersInsert";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customer.CustomerId);
            parameters.Add("CompanyName", customer.CompanyName);
            parameters.Add("ContactName", customer.ContactName);
            parameters.Add("ContactTitle", customer.ContactTitle);
            parameters.Add("Address", customer.Address);
            parameters.Add("City", customer.City);
            parameters.Add("Region", customer.Region);
            parameters.Add("PostalCode", customer.PostalCode);
            parameters.Add("Country", customer.Country);
            parameters.Add("Phone", customer.Phone);
            parameters.Add("Fax", customer.Fax);

            var result = dbConnection.Execute(query, param: parameters, commandType: CommandType.StoredProcedure);
            return result > 0;
        }
        public bool Update(Customer customer)
        {
            using var dbConnection = _context.CreateConnection();
            var query = "CustomersUpdate";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customer.CustomerId);
            parameters.Add("CompanyName", customer.CompanyName);
            parameters.Add("ContactName", customer.ContactName);
            parameters.Add("ContactTitle", customer.ContactTitle);
            parameters.Add("Address", customer.Address);
            parameters.Add("City", customer.City);
            parameters.Add("Region", customer.Region);
            parameters.Add("PostalCode", customer.PostalCode);
            parameters.Add("Country", customer.Country);
            parameters.Add("Phone", customer.Phone);
            parameters.Add("Fax", customer.Fax);

            var result = dbConnection.Execute(query, param: parameters, commandType: CommandType.StoredProcedure);
            return result > 0;
        }

        public bool Delete(string customerId)
        {
            using var dbConnection = _context.CreateConnection();
            var query = "CustomersDelete";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customerId);

            var result = dbConnection.Execute(query, param: parameters, commandType: CommandType.StoredProcedure);
            return result > 0;
        }

        public Customer Get(string customerId)
        {
            using var dbConnection = _context.CreateConnection();
            var query = "CustomersGetById";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customerId);

            var customer = dbConnection.QuerySingle<Customer>(query, param: parameters, commandType: CommandType.StoredProcedure);
            return customer;
        }
        public IEnumerable<Customer> GetAll()
        {
            using var dbConnection = _context.CreateConnection();
            var query = "CustomersList";

            var customers = dbConnection.Query<Customer>(query, commandType: CommandType.StoredProcedure);
            return customers;
        }
        public IEnumerable<Customer> GetAllWithPagination(int pageNumber, int pageSize)
        {
            using var dbConnection = _context.CreateConnection();
            var query = "CustomersListWithPagination";
            var parameters = new DynamicParameters();
            parameters.Add("pageNumber", pageNumber);
            parameters.Add("pageSize", pageSize);
            var customers = dbConnection.Query<Customer>(query, param: parameters, commandType: CommandType.StoredProcedure);
            return customers;
        }

        public int Count()
        {
            using var dbConnection = _context.CreateConnection();
            var query = "Select COUNT(*) from Customers";

            var count = dbConnection.ExecuteScalar<int>(query, commandType: CommandType.Text);
            return count;
        }

        #region Async

        public async Task<bool> InsertAsync(Customer customer)
        {
            using var dbConnection = _context.CreateConnection();
            var query = "CustomersInsert";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customer.CustomerId);
            parameters.Add("CompanyName", customer.CompanyName);
            parameters.Add("ContactName", customer.ContactName);
            parameters.Add("ContactTitle", customer.ContactTitle);
            parameters.Add("Address", customer.Address);
            parameters.Add("City", customer.City);
            parameters.Add("Region", customer.Region);
            parameters.Add("PostalCode", customer.PostalCode);
            parameters.Add("Country", customer.Country);
            parameters.Add("Phone", customer.Phone);
            parameters.Add("Fax", customer.Fax);

            var result = await dbConnection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);
            return result > 0;
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            using var dbConnection = _context.CreateConnection();
            var query = "CustomersUpdate";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customer.CustomerId);
            parameters.Add("CompanyName", customer.CompanyName);
            parameters.Add("ContactName", customer.ContactName);
            parameters.Add("ContactTitle", customer.ContactTitle);
            parameters.Add("Address", customer.Address);
            parameters.Add("City", customer.City);
            parameters.Add("Region", customer.Region);
            parameters.Add("PostalCode", customer.PostalCode);
            parameters.Add("Country", customer.Country);
            parameters.Add("Phone", customer.Phone);
            parameters.Add("Fax", customer.Fax);

            var result = await dbConnection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);
            return result > 0;
        }
        public async Task<bool> DeleteAsync(string customerId)
        {
            using var dbConnection = _context.CreateConnection();
            var query = "CustomersDelete";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customerId);

            var result = await dbConnection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);
            return result > 0;
        }
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            using var dbConnection = _context.CreateConnection();
            var query = "CustomersList";

            var customers = await dbConnection.QueryAsync<Customer>(query, commandType: CommandType.StoredProcedure);
            return customers;

        }

        public async Task<Customer> GetAsync(string customerId)
        {
            using var dbConnection = _context.CreateConnection();
            var query = "CustomersGetById";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customerId);

            var customer = await dbConnection.QuerySingleAsync<Customer>(query, param: parameters, commandType: CommandType.StoredProcedure);
            return customer;
        }



        public async Task<IEnumerable<Customer>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
        {
            using var dbConnection = _context.CreateConnection();
            var query = "CustomersListWithPagination";
            var parameters = new DynamicParameters();
            parameters.Add("pageNumber", pageNumber);
            parameters.Add("pageSize", pageSize);
            var customers = await dbConnection.QueryAsync<Customer>(query, param: parameters, commandType: CommandType.StoredProcedure);
            return customers;
        }

        public async Task<int> CountAsync()
        {
            using var dbConnection = _context.CreateConnection();
            var query = "Select COUNT(*) from Customers";

            var count = await dbConnection.ExecuteScalarAsync<int>(query, commandType: CommandType.Text);
            return count;
        }


        #endregion
    }
}