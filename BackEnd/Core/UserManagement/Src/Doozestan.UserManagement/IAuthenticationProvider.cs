
using Doozestan.Domain.ServiceRequest.Login;
using Doozestan.Domain.ServiceResponse.Login;

namespace Doozestan.UserManagement
{
    public interface IAuthenticationProvider
    {
        UserLoginResponse Authenticate(UserLoginRequest request);

    }
}
