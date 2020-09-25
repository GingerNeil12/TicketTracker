using System;
using System.Collections.Generic;
using System.Text;
using TicketTracker.Domain.Common;

namespace TicketTracker.Domain.Models
{
    public class Document : AuditableEntity
    {
        public string Id { get; set; }
        public string OriginalFileName { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public string TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
