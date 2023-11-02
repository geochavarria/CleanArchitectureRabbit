using AutoMapper;
using Ecommerce.Application.DTO;
using Ecommerce.Application.Interface.Persistence;
using Ecommerce.Application.Interface.UseCases;
using Ecommerce.Application.Validator;
using Ecommerce.Transversal.Common;

namespace Ecommerce.Application.UseCases.Users
{
    public class UsersApplication : IUserApplication
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UsersDtoValidator _usersDtoValidator;

        public UsersApplication(IUnitOfWork unitOfWork,
            IMapper mapper, UsersDtoValidator usersDtoValidator)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _usersDtoValidator = usersDtoValidator;

        }
        public Response<UserDTO> Authenticate(string username, string password)
        {
            var response = new Response<UserDTO>();
            var validation = _usersDtoValidator.Validate(new UserDTO()
            {
                UserName = username,
                Password = password
            });


            if (!validation.IsValid)
            {

                response.Message = "Errores de Validación";
                response.Errors = validation.Errors;
                return response;
            }

            try
            {
                var user = _unitOfWork.Users.Authenticate(username, password);
                response.Data = _mapper.Map<UserDTO>(user);
                response.IsSuccess = true;
                response.Message = "Autenticacion exitosa";

            }
            catch (InvalidOperationException e)
            {
                response.IsSuccess = true;
                response.Message = "Usuario no existe";
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }
    }
}
