using ec_utbildningar_assignment_datastorage.Services;

namespace TicketManagementConsole
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Welcome to the ticket management system.");
                Console.WriteLine("The current system works best with managing a local database.");
                Console.WriteLine("Please select an option:");
                Console.WriteLine("1. Create new ticket");
                Console.WriteLine("2. View all existing tickets");
                Console.WriteLine("3. View specific ticket");
                Console.WriteLine("4. Update ticket status");
                Console.WriteLine("5. Add comment to ticket");
                Console.WriteLine("6. View comments for ticket");
                Console.WriteLine("7. Exit");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Menu.CreateNewTicket();
                        break;
                    case "2":
                        Menu.ViewAllExistingTickets();
                        break;
                    case "3":
                        Menu.ViewSpecificTicket();
                        break;
                    case "4":
                        Menu.UpdateTicketStatus();
                        break;
                    case "5":
                        Menu.AddCommentToTicket();
                        break;
                    case "6":
                        Menu.ViewCommentsForTicket();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid option selected. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}
