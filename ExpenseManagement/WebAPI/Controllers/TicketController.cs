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
                return Results.Accepted("/process", _Services.GetReimbursementByUserID(ticket.author));
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
                return Results.Accepted("/users/id/{id}", ticket);
            }
            catch (ResourceNotFoundException)
            {
                return Results.BadRequest("NO Ticket has that ID");
            }
        }
    }
}
