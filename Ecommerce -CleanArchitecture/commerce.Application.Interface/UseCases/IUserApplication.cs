using Ecommerce.Application.DTO;
using Ecommerce.Transversal.Common;

namespace Ecommerce.Application.Interface.UseCases
{
    public interface IUserApplication
    {
        Response<UserDTO> Authenticate(string username, string password);
    }
}
