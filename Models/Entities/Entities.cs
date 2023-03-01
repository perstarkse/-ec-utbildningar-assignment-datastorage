using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ec_utbildningar_assignment_datastorage.Models.Entities
{
    public class Entities
    {
        public class Customer
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        }

        public class Ticket
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public DateTime CreatedTime { get; set; }
            public int StatusId { get; set; }
            public TicketStatus Status { get; set; }
            public int CustomerId { get; set; }
            public Customer Customer { get; set; }
            public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        }

        public class Comment
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public DateTime CreatedTime { get; set; }

            public int TicketId { get; set; }
            public Ticket Ticket { get; set; }
        }

        public class TicketStatus
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

    }
}
