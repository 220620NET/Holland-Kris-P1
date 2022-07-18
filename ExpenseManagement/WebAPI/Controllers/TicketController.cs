using Services;
using Models;
using CustomExceptions;
namespace WebAPI.Controllers
{
    public class TicketController
    {
        // Dependency Injection
        private readonly TicketServices _Services;
        public TicketController(TicketServices services)
        {
            _Services = services;
        }
        /// <summary>
        /// Controller that will create a new ticket
        /// </summary>
        /// <param name="newTicket"></param>
        /// <returns>Status Code 202 if successfully created</returns>
        /// <remarks>returns Status Code 409 if the ticket was improperly submitted and not created</remarks>
        public IResult Submit(Tickets newTicket)
        {
            try
            {
                bool all = _Services.SubmitReimbursement(newTicket);
                return Results.Accepted("/submit", _Services.GetReimbursementByUserID(newTicket.author));
            }
            catch (ResourceNotFoundException)
            {
                return Results.Conflict("We could not submit that ticket.");
            }
            catch (UsernameNotAvailable)
            {
                return Results.Conflict("There is no user with that ID.");
            }
        }
        /// <summary>
        /// Controller to update a ticket
        /// </summary>
        /// <remarks>returns Status Code 400 if ticket has already been update to a new status<br/>
        ///             returns Status Code 409 if the ticket could not be updated or if there is no user with that ID</remarks>
        /// <param name="ticket"></param>
        /// <returns>Status Code 202 if ticket updated</returns>
        public IResult Process(Tickets ticket)
        {
            try
            {

                bool all = _Services.UpdateReimbursement(ticket);
                if (all)
                {
                    return Results.Accepted("/process", _Services.GetReimbursementByID(ticket.ticketNum));
                }
                return Results.BadRequest($"That Ticket has already been {_Services.GetReimbursementByID(ticket.ticketNum).status}.");
            }
            catch (ResourceNotFoundException)
            {
                return Results.Conflict("We could not update that ticket.");
            }
            catch (UsernameNotAvailable)
            {
                return Results.Conflict("There is no user with that ID.");
            }
        }
        /// <summary>
        /// Controller to get a particular ticket
        /// </summary>
        /// <remarks>returns Status Code 400 if there is no ticket with that Id</remarks>
        /// <param name="ticketNum"></param>
        /// <returns>Status Code 202 if ticket exists and can be recieved</returns>
        public IResult GetTicketByTicketNum(int ticketNum)
        {
            try
            {
                Tickets ticket = _Services.GetReimbursementByID(ticketNum);
                return Results.Accepted("/tickets/id/{id}", ticket);
            }
            catch (ResourceNotFoundException)
            {
                return Results.BadRequest("That ticket hasn't been made yet");
            }
        }
        /// <summary>
        /// Controller to get a particular set of tickets
        /// </summary>
        /// <remarks>returns Status Code 400 if there are no tickets from that author</remarks>
        /// <param name="authorID"></param>
        /// <returns>Status Code 202 with available tickets</returns>
        public IResult GetTicketByAuthor(int authorID)
        {
            try
            {
                List<Tickets> tickets = _Services.GetReimbursementByUserID(authorID);
                return Results.Accepted("/tickets/author/{authorID}", tickets);
            }
            catch (ResourceNotFoundException)
            {
                return Results.BadRequest("That user hasn't made any tickets.");
            }
        }
        /// <summary>
        /// Controller to get a particular set of tickets
        /// </summary>
        /// <remarks>returns Status Code 400 if there are no tickets with that status</remarks>
        /// <param name="state"></param>
        /// <returns>Status code 202 with a list of those tickets with that status</returns>
        public IResult GetTicketByStatus(string state)
        {
            int s= new Tickets().StateToNum(state);
            try
            {
                List<Tickets> ticket = _Services.GetReimbursementByStatus(s);
                if (ticket.Count == 0)
                {
                    throw new ResourceNotFoundException();
                }
                return Results.Accepted("/tickets/status/{state}", ticket);
            }
            catch (ResourceNotFoundException)
            {
                return Results.BadRequest($"There are no tickets that are {new Tickets().NumToState(s)}.");
            }
        }
        public IResult GetAllTickets()
        {
            try
            {
                List<Tickets> all = _Services.GetAllTickets();
                return Results.Accepted("/tickets", all);
            }
            catch (ResourceNotFoundException)
            {
                return Results.BadRequest("There are no tickets");
            }
        }
    }
}
