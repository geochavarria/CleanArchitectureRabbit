using AutoMapper;
using commerce.Application.Interface.Infraestructure;
using Ecommerce.Application.DTO;
using Ecommerce.Application.Interface.Persistence;
using Ecommerce.Application.Interface.UseCases;
using Ecommerce.Application.Validator;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Events;
using Ecommerce.Transversal.Common;
using System.Text.Json;

namespace Ecommerce.Application.UseCases.Discounts
{
    public class DiscountsApplication : IDiscountApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DiscountDtoValidator _discountValitor;
        private readonly IEventBus _eventBus;
        private readonly INotification _notification;
        public DiscountsApplication(IUnitOfWork unitOfWork, IMapper mapper, 
            DiscountDtoValidator validationRules, 
            IEventBus eventBus,
            INotification notification)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _discountValitor = validationRules;
            _eventBus = eventBus;
            _notification = notification;
        }
        public async Task<Response<bool>> Create(DiscountDTO discountDTO, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();
            try
            {
                var validator = await _discountValitor.ValidateAsync(discountDTO, cancellationToken);
                if (!validator.IsValid)
                {
                    response.Message = "Errores de validacion";
                    response.Errors = validator.Errors;
                    return response;
                }

                var discount = _mapper.Map<Discount>(discountDTO);
                await _unitOfWork.Discounts.InsertAsync(discount);

                var x = await _unitOfWork.Save(cancellationToken);
                response.Data = x > 0;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro Exitoso";

                    //Publicar el evento:
                    var discountCreateEvent = _mapper.Map<DiscountCreatedEvent>(discount);
                    _eventBus.Publish(discountCreateEvent);

                    //enviamos Correo
                    await _notification.SendMailAsync(response.Message, JsonSerializer.Serialize(discount), cancellationToken);


                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                await _notification.SendMailAsync(response.Message, JsonSerializer.Serialize(response), cancellationToken);

            }

            return response;
        }

        public async Task<Response<bool>> Delete(int id, CancellationToken cancellationToken)
        {
            var response = new Response<bool>();
            try
            {
                await _unitOfWork.Discounts.DeleteAsync(id.ToString());
                response.Data = await _unitOfWork.Save(cancellationToken) > 0;
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

        public async Task<Response<DiscountDTO>> Get(int id, CancellationToken cancellationToken = default)
        {
            var response = new Response<DiscountDTO>();
            try
            {
                var discount = await _unitOfWork.Discounts.GetAsync(id.ToString(), cancellationToken);
                if (discount == null)
                {
                    response.IsSuccess = true;
                    response.Message = "Descuento no existe...";
                    return response;
                }

                response.Data = _mapper.Map<DiscountDTO>(discount);
                response.IsSuccess = true;
                response.Message = "Consulta exitosa...";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<IEnumerable<DiscountDTO>>> GetAll(CancellationToken cancellationToken = default)
        {
            var response = new Response<IEnumerable<DiscountDTO>>();
            try
            {
                var discount = await _unitOfWork.Discounts.GetAllAsync(cancellationToken);

                response.Data = _mapper.Map<IEnumerable<DiscountDTO>>(discount);
                response.IsSuccess = true;
                response.Message = "Consulta exitosa...";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResponsePagination<IEnumerable<DiscountDTO>>> GetAllWithPagination(int pageNumber, int pageSize)
        {
            var response = new ResponsePagination<IEnumerable<DiscountDTO>>();
            try
            {
                var count = await _unitOfWork.Discounts.CountAsync();
                var customers = await _unitOfWork.Discounts.GetAllWithPaginationAsync(pageNumber, pageSize);
                response.Data = _mapper.Map<IEnumerable<DiscountDTO>>(customers);

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

        public async Task<Response<bool>> Update(DiscountDTO discountDTO, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();
            try
            {
                var validator = await _discountValitor.ValidateAsync(discountDTO, cancellationToken);
                if (!validator.IsValid)
                {
                    response.Message = "Errores de validacion";
                    response.Errors = validator.Errors;
                    return response;
                }

                var discount = _mapper.Map<Discount>(discountDTO);
                await _unitOfWork.Discounts.UpdateAsync(discount);

                response.Data = await _unitOfWork.Save(cancellationToken) > 0;
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
