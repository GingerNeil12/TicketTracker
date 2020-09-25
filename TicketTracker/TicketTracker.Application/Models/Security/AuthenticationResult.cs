namespace TicketTracker.Application.Models.Security
{
    public class AuthenticationResult
    {
        public AuthenticationResult
        (
            AuthenticationCode code,
            string message
        )
        {
            Code = code;
            Message = message;
        }

        public AuthenticationCode Code { get; }
        public string Message { get; }
    }
}
