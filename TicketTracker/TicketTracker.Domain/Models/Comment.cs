using System;
using System.Collections.Generic;
using System.Text;
using TicketTracker.Domain.Common;

namespace TicketTracker.Domain.Models
{
    public class Comment : AuditableEntity
    {
        public int Id { get; set; }
        public string TicketId { get; set; }
        public string Body { get; set; }
        public string UserId { get; set; }
        public Board Board { get; set; }
        public User User { get; set; }
    }
}
