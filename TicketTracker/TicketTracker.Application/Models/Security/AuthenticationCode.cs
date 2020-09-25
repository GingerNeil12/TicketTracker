namespace TicketTracker.Application.Models.Security
{
    public enum AuthenticationCode
    {
        Success,
        EmailOrPasswordIncorrect,
        AccountLocked
    }
}
