using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TicketTracker.Domain.Common;

namespace TicketTracker.Domain.Models
{
    public class Board : AuditableEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public List<Category> Categories { get; set; }
        public List<BoardUser> Users { get; set; }
    }
}
