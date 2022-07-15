using Services;
using Models;
using CustomExceptions;
namespace WebAPI.Controllers
{
    public class TicketController
    {
        private readonly TicketServices _Services;
        public TicketController(TicketServices services)
        {
            _Services = services;
        }
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
                return Results.Conflict("We could not submit that ticket.");
            }
            catch (UsernameNotAvailable)
            {
                return Results.Conflict("There is no user with that ID.");
            }
        }
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
    }
}
