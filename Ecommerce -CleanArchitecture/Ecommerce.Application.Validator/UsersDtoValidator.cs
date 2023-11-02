using Ecommerce.Application.DTO;
using FluentValidation;

namespace Ecommerce.Application.Validator
{
    public class UsersDtoValidator : AbstractValidator<UserDTO>
    {
        public UsersDtoValidator()
        {
            RuleFor(u => u.UserName).NotEmpty().NotNull();
            RuleFor(u => u.Password).NotEmpty().NotNull();


        }
    }
}