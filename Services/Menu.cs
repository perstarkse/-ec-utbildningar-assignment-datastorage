using ec_utbildningar_assignment_datastorage.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ec_utbildningar_assignment_datastorage.Models.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace ec_utbildningar_assignment_datastorage.Services
{
    public class Menu
    {
        public static void CreateNewTicket()
        {
            var context = new DataContext();
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("First you have to choose your user.");
                Console.WriteLine("1. Choose an existing user.");
                Console.WriteLine("2. Create a new user.");
                Console.WriteLine("3. Return to main menu.");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Enter the email address of the customer:");
                        string email = Console.ReadLine();
                        var existingCustomer = context.Customers.FirstOrDefault(c => c.Email == email);
                        if (existingCustomer != null)
                        {
                            Console.WriteLine($"Found customer {existingCustomer.FirstName} {existingCustomer.LastName} ({existingCustomer.Email}).");
                            Console.WriteLine("Enter ticket description:");
                            string description = Console.ReadLine();

                            var ticket = new Ticket
                            {
                                Description = description,
                                CreatedTime = DateTime.Now,
                                Status = context.TicketStatuses.First(),
                                Customer = existingCustomer,
                            };

                            context.Tickets.Add(ticket);
                            context.SaveChanges();

                            Console.WriteLine("New ticket created with ID {0}.", ticket.Id);
                            ExitToMenu();
                            isRunning = false;
                        }
                        else
                        {
                            Console.WriteLine($"Could not find customer with email address {email}.");
                            ExitToMenu();
                        }
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Enter customer first name:");
                        string firstName = Console.ReadLine();
                        Console.WriteLine("Enter customer last name:");
                        string lastName = Console.ReadLine();
                        Console.WriteLine("Enter customer email:");
                        string newEmail = Console.ReadLine();
                        Console.WriteLine("Enter customer phone number:");
                        string phoneNumber = Console.ReadLine();

                        var customer = new Customer
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            Email = newEmail,
                            PhoneNumber = phoneNumber
                        };
                        context.Customers.Add(customer);
                        context.SaveChanges();

                        Console.WriteLine("New customer created with ID {0}.", customer.Id);

                        Console.WriteLine("Enter ticket description:");
                        string newDescription = Console.ReadLine();

                        var ticket2 = new Ticket
                        {
                            Description = newDescription,
                            CreatedTime = DateTime.Now,
                            Status = context.TicketStatuses.First(),
                            Customer = customer,
                        };

                        context.Tickets.Add(ticket2);
                        context.SaveChanges();

                        Console.WriteLine("New ticket created with ID {0}.", ticket2.Id);
                        ExitToMenu();
                        isRunning = false;
                        break;
                    case "3":
                        isRunning = false;
                        return;
                    default:
                        Console.WriteLine("Enter an appropriate command.");
                        break;
                }
            }
        }


        public static void ViewAllExistingTickets()
        {
            using (var context = new DataContext())
            {
                var tickets = context.Tickets
                    .Include(t => t.Status)
                    .Include(t => t.Customer)
                    .ToList();
                Console.Clear();
                Console.WriteLine("Existing tickets:");
                foreach (var ticket in tickets)
                {
                    Console.WriteLine("ID: {0}", ticket.Id);
                    Console.WriteLine("Customer: {0} {1}", ticket.Customer.FirstName, ticket.Customer.LastName);
                    Console.WriteLine("Description: {0}", ticket.Description);
                    Console.WriteLine("Status: {0}", ticket.Status.Name);
                    Console.WriteLine();
                }

                if (tickets.Count == 0)
                {
                    Console.WriteLine("No tickets found.");
                }
                ExitToMenu();
            }
        }

        public static void UpdateTicketStatus()
        {
            using (var context = new DataContext())
            {
                Console.Clear();
                Console.WriteLine("Enter ticket ID:");
                int id = int.Parse(Console.ReadLine());
                var ticket = context.Tickets.FirstOrDefault(t => t.Id == id);

                if (ticket == null)
                {
                    Console.WriteLine("Ticket not found.");
                    return;
                }

                Console.WriteLine("Enter new status ID (1 = Not Started, 2 = In Progress, 3 = Completed):");
                int statusId = int.Parse(Console.ReadLine());
                var status = context.TicketStatuses.FirstOrDefault(s => s.Id == statusId);
                if (status == null)
                {
                    Console.WriteLine("Invalid status input.");
                    return;
                }

                ticket.StatusId = status.Id;

                context.SaveChanges();

                Console.WriteLine($"Ticket {ticket.Id} status updated to {status.Name}.");
                ExitToMenu();
            }
        }

        public static void AddCommentToTicket()
        {
            Console.Clear();
            Console.Write("Enter ticket ID: ");
            var ticketIdInput = Console.ReadLine();

            if (!int.TryParse(ticketIdInput, out var ticketId))
            {
                Console.WriteLine("Invalid ticket ID input.");
                return;
            }

            using (var context = new DataContext())
            {
                var ticket = context.Tickets.Include(t => t.Customer).FirstOrDefault(t => t.Id == ticketId);

                if (ticket == null)
                {
                    Console.WriteLine($"Ticket with ID {ticketId} not found.");
                    return;
                }

                Console.WriteLine($"Ticket {ticket.Id} found for customer {ticket.Customer.FirstName} {ticket.Customer.LastName}.");
                Console.Write("Enter comment text: ");
                var commentText = Console.ReadLine();

                var comment = new Comment
                {
                    Text = commentText,
                    CreatedTime = DateTime.Now,
                    TicketId = ticket.Id
                };

                context.Comments.Add(comment);
                context.SaveChanges();

                Console.WriteLine($"Comment added to ticket {ticket.Id}.");
                ExitToMenu();
            }
        }

        public static void ViewCommentsForTicket()
        {
            Console.Clear();
            Console.Write("Enter ticket ID: ");
            var ticketIdInput = Console.ReadLine();

            if (!int.TryParse(ticketIdInput, out var ticketId))
            {
                Console.WriteLine("Invalid ticket ID input.");
                ExitToMenu();
                return;
            }

            using (var context = new DataContext())
            {
                var ticket = context.Tickets.Include(t => t.Customer).Include(t => t.Comments).FirstOrDefault(t => t.Id == ticketId);

                if (ticket == null)
                {
                    Console.WriteLine($"Ticket with ID {ticketId} not found.");
                    ExitToMenu();
                    return;
                }

                Console.WriteLine($"Ticket {ticket.Id} found for customer {ticket.Customer.FirstName} {ticket.Customer.LastName}.");
                Console.WriteLine("Comments:");

                foreach (var comment in ticket.Comments)
                {
                    Console.WriteLine($"{comment.CreatedTime} - {comment.Text}");
                }
                ExitToMenu();
            }
        }
        public static void ViewSpecificTicket()
        {
            Console.Clear();
            Console.Write("Enter ticket ID: ");
            var ticketIdInput = Console.ReadLine();

            if (!int.TryParse(ticketIdInput, out var ticketId))
            {
                Console.WriteLine("Invalid ticket ID input.");
                ExitToMenu();
                return;
            }
            using (var context = new DataContext())
            {
                var ticket = context.Tickets.Include(t => t.Status).Include(t => t.Comments).Include(t => t.Customer).FirstOrDefault(t => t.Id == ticketId);

                if (ticket == null)
                {
                    Console.WriteLine($"Ticket with ID {ticketId} not found.");
                    ExitToMenu();
                    return;
                }

                Console.WriteLine($"Ticket {ticket.Id} found for customer {ticket.Customer.FirstName} {ticket.Customer.LastName}.");

                Console.WriteLine($"Ticket status: {ticket.Status.Name}");

                Console.WriteLine("Ticket description:");
                Console.WriteLine(ticket.Description);

                Console.WriteLine("Comments:");

                foreach (var comment in ticket.Comments)
                {
                    Console.WriteLine($"{comment.CreatedTime} - {comment.Text}");
                }
                ExitToMenu();
            }
        }

        public static void ExitToMenu()
        {
            Console.Write("Press key to proceed.");
            Console.ReadKey();
        }
    }
}
