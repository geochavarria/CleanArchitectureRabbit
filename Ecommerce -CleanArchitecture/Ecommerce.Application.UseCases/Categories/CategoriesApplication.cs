using AutoMapper;
using Ecommerce.Application.DTO;
using Ecommerce.Application.Interface.Persistence;
using Ecommerce.Application.Interface.UseCases;
using Ecommerce.Transversal.Common;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Ecommerce.Application.UseCases.Categories
{
    public class CategoriesApplication : ICategoriesApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public CategoriesApplication(IUnitOfWork unitOfWork, IMapper mapper, IDistributedCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Response<IEnumerable<CategoryDTO>>> GetAll()
        {
            var response = new Response<IEnumerable<CategoryDTO>>();
            var cackeKey = "categoriesList";
            try
            {
                var redisCategories = await _cache.GetAsync(cackeKey);
                if (redisCategories != null)
                {
                    response.Data = JsonSerializer.Deserialize<IEnumerable<CategoryDTO>>(redisCategories)!;
                }
                else
                {
                    var categories = await _unitOfWork.Categories.GetAll();
                    response.Data = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
                    if (response.Data != null)
                    {
                        var serealizedCategories = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response.Data));
                        var options = new DistributedCacheEntryOptions()
                            .SetAbsoluteExpiration(DateTime.Now.AddHours(8))
                            .SetSlidingExpiration(TimeSpan.FromMinutes(60));

                        await _cache.SetAsync(cackeKey, serealizedCategories, options);
                    }
                }

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta exitosa";
                }

            }
            catch (Exception es)
            {
                response.Message = es.Message;
            }
            return response;

        }
    }
}
