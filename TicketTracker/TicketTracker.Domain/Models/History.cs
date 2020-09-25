using System;
using System.Collections.Generic;
using System.Text;
using TicketTracker.Domain.Common;

namespace TicketTracker.Domain.Models
{
    public class History : AuditableEntity
    {
        public int Id { get; set; }
        public string TicketId { get; set; }
        public string Description { get; set; }
        public DateTime HappenedOn { get; set; }
        public bool IsImportant { get; set; }
        public Ticket Ticket { get; set; }
    }
}
