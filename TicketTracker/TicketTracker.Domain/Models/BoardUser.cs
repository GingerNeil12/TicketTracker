using System;
using System.Collections.Generic;
using System.Text;
using TicketTracker.Domain.Common;
using TicketTracker.Domain.Enums;

namespace TicketTracker.Domain.Models
{
    public class BoardUser : AuditableEntity
    {
        public string BoardId { get; set; }
        public string UserId { get; set; }
        public BoardRole Role { get; set; }
        public Board Board { get; set; }
        public User User { get; set; }
    }
}
