using System;
using System.Collections.Generic;
using System.Text;
using TicketTracker.Domain.Common;

namespace TicketTracker.Domain.Models
{
    public class User : AuditableEntity
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<BoardUser> Boards { get; set; }
        public List<Ticket> ReportedTickets { get; set; }
        public List<Ticket> AssignedTickets { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
