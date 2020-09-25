using System;
using System.Collections.Generic;
using System.Text;
using TicketTracker.Domain.Common;

namespace TicketTracker.Domain.Models
{
    public class Category : AuditableEntity
    {
        public string Id { get; set; }
        public string BoardId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
        public string Colour { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
