using Ecommerce.Application.DTO;
using Ecommerce.Application.Interface.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Annotations;

using Ecommerce.Transversal.Common;
using Asp.Versioning;

namespace Ecommerce.Services.WebApi.Controllers.v2
{
    [Route("Api/v{version:apiVersion}/[controller]")]
    [EnableRateLimiting("fixedWindow")]
    [ApiController]
    [Authorize]
    [ApiVersion("2.0")]
    [SwaggerTag("Get Categories of products")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesApplication _categoriesApplication;

        public CategoriesController(ICategoriesApplication categoriesApplication)
        {
            _categoriesApplication = categoriesApplication;
        }


        [HttpGet("GetAll")]
        [SwaggerOperation(Summary = "Obtener categorias",
            Description = "Este retorna todas las categorias",
            OperationId = "GetAll",
            Tags = new string[] { "GetAll" })]
        [SwaggerResponse(200, "List of Categories", typeof(Response<IEnumerable<CategoryDTO>>))]
        [SwaggerResponse(404, "Not Found Categories")]
        public async Task<IActionResult> GetAllAsyn()
        {
            var response = await _categoriesApplication.GetAll();
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

    }
}
