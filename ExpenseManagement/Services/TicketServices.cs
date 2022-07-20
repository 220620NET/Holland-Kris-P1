using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DataAccess;
using CustomExceptions;
namespace Services
{
    public class TicketServices
    {
        //Dependency injection
        private readonly ITicketDAO _ticketDAO;
        public TicketServices(ITicketDAO ticketDAO)
        {
            _ticketDAO = ticketDAO;
        }

        /// <summary>
        /// Service to create a ticket, will be used by the employee
        /// </summary>
        /// <param name="newTicket"></param>
        /// <returns>boolean where true if ticket was created, false otherwise </returns>
        /// <exception cref="ResourceNotFoundException">Occurs if information provided was improper or if the table could not be located</exception>
        public bool SubmitReimbursement(Tickets newTicket)
        {
            try
            {
                bool s =  _ticketDAO.CreateTicket(newTicket);
                if (s)
                {
                    return true;
                }
                else { return false; }
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();

            }
            catch (UsernameNotAvailable)
            {
                throw new UsernameNotAvailable();
            }
        }
        
        /// <summary>
        /// Service to update a ticket, will be used by the manage changing a ticket to either approved or denied
        /// </summary>
        /// <param name="update"></param>
        /// <returns>boolean where true if ticket was successfully updated, false otherwise</returns>
        /// <exception cref="ResourceNotFoundException">Occurs if the data could not be updated, whether that ticket doesn't exist yet or not</exception>
        public bool UpdateReimbursement(Tickets update)
        {
            try
            {
                return _ticketDAO.UpdateTicket(update);
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();

            }
            catch (UsernameNotAvailable)
            {
                throw new UsernameNotAvailable();
            }
        }

        /// <summary>
        /// Service to retrieve a specific ticket by its individual ID
        /// </summary>
        /// <param name="ticketID"></param>
        /// <returns>Ticket with specified ticketID</returns>
        /// <exception cref="ResourceNotFoundException">Occurs if that ticket hasn't been made yet</exception>
        public Tickets GetReimbursementByID(int ticketID)
        {
            try
            {
                return _ticketDAO.GetTicketsById(ticketID);
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
        }
  
        /// <summary>
        /// Service that will retrieve a specific group of tickets authored by the same employee
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>List of tickets from a specific author</returns>
        /// <exception cref="ResourceNotFoundException">Occurs if that user has not made any tickets</exception>
        public List<Tickets> GetReimbursementByUserID(int userID)
        {
            try
            {
                return _ticketDAO.GetTicketsByAuthor(userID);
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
        }

        /// <summary>
        /// Service that will return a group of tickets based on the status
        /// </summary>
        /// <param name="state"></param>
        /// <returns>List of tickets with a specified status</returns>
        /// <exception cref="ResourceNotFoundException">Occurs when there are no such tickets with that status</exception>
        public List<Tickets> GetReimbursementByStatus(int state)
        {
            try
            {
                return _ticketDAO.GetTicketsByStatus(state);
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
        }
        /// <summary>
        /// This will retrieve all tickets in the database
        /// </summary>
        /// <returns>All tickets in the database</returns>
        /// <exception cref="ResourceNotFoundException">There are no tickets in the database</exception>
        public List<Tickets> GetAllTickets()
        {
            try
            {
                return _ticketDAO.GetAllTickets();
            }
            catch (ResourceNotFoundException)
            {
                throw new ResourceNotFoundException();
            }
        }
    }
}
