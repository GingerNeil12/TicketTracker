using System;

namespace TicketTracker.Application.Interfaces.Common
{
    public interface ICurrentDateTime
    {
        DateTime Now { get; }
    }
}
