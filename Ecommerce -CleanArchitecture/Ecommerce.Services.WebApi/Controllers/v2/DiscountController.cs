using Asp.Versioning;
using Ecommerce.Application.DTO;
using Ecommerce.Application.Interface.UseCases;
using Ecommerce.Application.UseCases.Customers;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Services.WebApi.Controllers.v2
{

    [Route("Api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class DiscountController : ControllerBase
    {

        private readonly IDiscountApplication _discountApplication;
        public DiscountController(IDiscountApplication discountApplication)
        {
            _discountApplication = discountApplication;
        }


        #region "Metodos Asincronos"

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] DiscountDTO discountDTO)
        {
            if (discountDTO == null)
            {
                return BadRequest();
            }

            var response = await _discountApplication.Create(discountDTO);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] DiscountDTO discountDTO)
        {

            var discountExists = await _discountApplication.Get(id);
            if (discountExists == null)
                return NotFound(discountExists);


            if (discountExists == null)
            {
                return BadRequest();
            }

            var response = await _discountApplication.Update(discountDTO);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _discountApplication.Delete(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {

            var response = await _discountApplication.Get(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _discountApplication.GetAll();
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }




        [HttpGet("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var response = await _discountApplication.GetAllWithPagination(pageNumber, pageSize);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }
        #endregion
    }
}
