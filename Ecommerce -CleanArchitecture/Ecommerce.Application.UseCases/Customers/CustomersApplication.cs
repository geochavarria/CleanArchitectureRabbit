using AutoMapper;
using Ecommerce.Application.DTO;
using Ecommerce.Application.Interface.Persistence;
using Ecommerce.Application.Interface.UseCases;
using Ecommerce.Domain.Entities;
using Ecommerce.Transversal.Common;

namespace Ecommerce.Application.UseCases.Customers
{
    public class CustomersApplication : ICustomersApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly IAppLogger<CustomersApplication> _logger;
        public CustomersApplication(IUnitOfWork unitOfWork,
            IMapper mapper, IAppLogger<CustomersApplication> appLogger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = appLogger;
        }

        public Response<bool> Delete(string customerId)
        {
            var response = new Response<bool>();
            try
            {
                response.Data = _unitOfWork.Customers.Delete(customerId);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Eliminacion Exitosa";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<bool>> DeleteAsync(string customerId)
        {
            var response = new Response<bool>();
            try
            {
                response.Data = await _unitOfWork.Customers.DeleteAsync(customerId);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Eliminacion Exitosa";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public Response<CustomerDto> Get(string customerId)
        {
            var response = new Response<CustomerDto>();
            try
            {
                var customer = _unitOfWork.Customers.Get(customerId);
                response.Data = _mapper.Map<CustomerDto>(customer);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public Response<IEnumerable<CustomerDto>> GetAll()
        {
            var response = new Response<IEnumerable<CustomerDto>>();
            try
            {
                var customers = _unitOfWork.Customers.GetAll();
                response.Data = _mapper.Map<IEnumerable<CustomerDto>>(customers);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa";

                    _logger.LogInformation("Consulta Exitosa");
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                _logger.LogError(ex.Message, ex);
            }
            return response;
        }

        public ResponsePagination<IEnumerable<CustomerDto>> GetAllWithPagination(int pageNumber, int pageSize)
        {
            var response = new ResponsePagination<IEnumerable<CustomerDto>>();
            try
            {
                var count = _unitOfWork.Customers.Count();
                var customers = _unitOfWork.Customers.GetAllWithPagination(pageNumber, pageSize);
                response.Data = _mapper.Map<IEnumerable<CustomerDto>>(customers);

                if (response.Data != null)
                {
                    response.PageNumber = pageNumber;
                    response.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                    response.TotalCount = count;
                    response.IsSuccess = true;
                    response.Message = "Consulta paginada exitosa!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<IEnumerable<CustomerDto>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<CustomerDto>>();
            try
            {
                var customers = await _unitOfWork.Customers.GetAllAsync();
                response.Data = _mapper.Map<IEnumerable<CustomerDto>>(customers);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }



        public async Task<ResponsePagination<IEnumerable<CustomerDto>>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
        {
            var response = new ResponsePagination<IEnumerable<CustomerDto>>();
            try
            {
                var count = await _unitOfWork.Customers.CountAsync();
                var customers = await _unitOfWork.Customers.GetAllWithPaginationAsync(pageNumber, pageSize);
                response.Data = _mapper.Map<IEnumerable<CustomerDto>>(customers);

                if (response.Data != null)
                {
                    response.PageNumber = pageNumber;
                    response.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                    response.TotalCount = count;
                    response.IsSuccess = true;
                    response.Message = "Consulta paginada exitosa!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<CustomerDto>> GetAsync(string customerId)
        {
            var response = new Response<CustomerDto>();
            try
            {
                var customer = await _unitOfWork.Customers.GetAsync(customerId);
                response.Data = _mapper.Map<CustomerDto>(customer);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public Response<bool> Insert(CustomerDto customerDto)
        {
            var response = new Response<bool>();
            try
            {
                var customer = _mapper.Map<Customer>(customerDto);
                response.Data = _unitOfWork.Customers.Insert(customer);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro Exitoso";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<bool>> InsertAsync(CustomerDto customerDto)
        {
            var response = new Response<bool>();
            try
            {
                var customer = _mapper.Map<Customer>(customerDto);
                response.Data = await _unitOfWork.Customers.InsertAsync(customer);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro Exitoso";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public Response<bool> Update(CustomerDto customerDto)
        {
            var response = new Response<bool>();
            try
            {
                var customer = _mapper.Map<Customer>(customerDto);
                response.Data = _unitOfWork.Customers.Update(customer);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Actualización Exitoso";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<bool>> UpdateAsync(CustomerDto customerDto)
        {
            var response = new Response<bool>();
            try
            {
                var customer = _mapper.Map<Customer>(customerDto);
                response.Data = await _unitOfWork.Customers.UpdateAsync(customer);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Actualización Exitoso";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
    }
}