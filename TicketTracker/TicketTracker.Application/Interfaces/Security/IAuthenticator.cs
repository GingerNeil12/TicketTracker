using TicketTracker.Application.Models.Security;
using TicketTracker.Shared.ViewModels.Security;

namespace TicketTracker.Application.Interfaces.Security
{
    public interface IAuthenticator
    {
        AuthenticationResult Authenticate(LoginDto loginDto);
    }
}
