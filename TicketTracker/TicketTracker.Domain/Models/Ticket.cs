using System;
using System.Collections.Generic;
using System.Text;
using TicketTracker.Domain.Common;
using TicketTracker.Domain.Enums;

namespace TicketTracker.Domain.Models
{
    public class Ticket : AuditableEntity
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string ReportedBy { get; set; }
        public string AssignedTo { get; set; }
        public DateTime DueDate { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public string CategoryId { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Document> Documents { get; set; }
        public List<History> History { get; set; }
    }
}
