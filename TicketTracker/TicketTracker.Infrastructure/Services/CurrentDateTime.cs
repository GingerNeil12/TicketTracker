using System;
using TicketTracker.Application.Interfaces.Common;

namespace TicketTracker.Infrastructure.Services
{
    public class CurrentDateTime : ICurrentDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
