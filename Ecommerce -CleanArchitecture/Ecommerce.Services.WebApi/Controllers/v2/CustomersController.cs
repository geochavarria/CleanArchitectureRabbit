using Asp.Versioning;
using Ecommerce.Application.DTO;
using Ecommerce.Application.Interface.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Services.WebApi.Controllers.v2
{
    [Authorize]
    [Route("Api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersApplication _customersApplication;

        public CustomersController(ICustomersApplication customersApplication)
        {
            _customersApplication = customersApplication;
        }



        #region "Metodos Sincronos"

        [HttpPost]
        public IActionResult Insert([FromBody] CustomerDto customerDto)
        {
            if (customerDto == null)
            {
                return BadRequest();
            }

            var response = _customersApplication.Insert(customerDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpPut]
        public IActionResult Update(string customerId, [FromBody] CustomerDto customersDto)
        {
            var customerDto = _customersApplication.Get(customerId);
            if (customerDto == null)
                return NotFound(customerDto.Message);


            if (customersDto == null)
            {
                return BadRequest();
            }

            var response = _customersApplication.Update(customersDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpDelete("{customerId}")]
        public IActionResult Delete(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                return BadRequest();
            }

            var response = _customersApplication.Delete(customerId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpGet("{customerId}")]
        public IActionResult Get(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                return BadRequest();
            }

            var response = _customersApplication.Get(customerId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _customersApplication.GetAll();
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }


        [HttpGet("GetAllWithPagination")]
        public IActionResult GetAllWithPagination([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var response = _customersApplication.GetAllWithPagination(pageNumber, pageSize);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }
        #endregion


        #region "Metodos Asincronos"

        [HttpPost("[action]")]
        public async Task<IActionResult> InsertAsync([FromBody] CustomerDto customerDto)
        {
            if (customerDto == null)
            {
                return BadRequest();
            }

            var response = await _customersApplication.InsertAsync(customerDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateAsync(string customerId, [FromBody] CustomerDto customersDto)
        {

            var customerDto = await _customersApplication.GetAsync(customerId);
            if (customerDto == null)
                return NotFound(customerDto.Message);


            if (customersDto == null)
            {
                return BadRequest();
            }

            var response = await _customersApplication.UpdateAsync(customersDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpDelete("[action]/{customerId}")]
        public async Task<IActionResult> DeleteAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                return BadRequest();
            }

            var response = await _customersApplication.DeleteAsync(customerId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpGet("[action]/{customerId}")]
        public async Task<IActionResult> GetAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                return BadRequest();
            }

            var response = await _customersApplication.GetAsync(customerId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _customersApplication.GetAllAsync();
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }



        [HttpGet("GetAllWithPaginationAsync")]
        public async Task<IActionResult> GetAllWithPaginationAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var response = await _customersApplication.GetAllWithPaginationAsync(pageNumber, pageSize);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }
        #endregion
    }
}
