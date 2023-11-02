using Asp.Versioning;
using Ecommerce.Application.DTO;
using Ecommerce.Application.Interface.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Services.WebApi.Controllers.v1
{
    [Authorize]
    //[Route("Api/[controller]")]
    [Route("Api/v{version:apiVersion}/[controller]")] //Segmente URL API Version
    [ApiController]
    ///[ApiVersion("1.0")]
    [ApiVersion("1.0", Deprecated = true)]
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
        public IActionResult Update([FromBody] CustomerDto customerDto)
        {
            if (customerDto == null)
            {
                return BadRequest();
            }

            var response = _customersApplication.Update(customerDto);
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
        public async Task<IActionResult> UpdateAsync([FromBody] CustomerDto customerDto)
        {
            if (customerDto == null)
            {
                return BadRequest();
            }

            var response = await _customersApplication.UpdateAsync(customerDto);
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

        #endregion
    }
}
