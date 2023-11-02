using Ecommerce.Application.DTO;
using FluentValidation;

namespace Ecommerce.Application.Validator
{
    public class DiscountDtoValidator : AbstractValidator<DiscountDTO>
    {
        public DiscountDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.Percent).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
