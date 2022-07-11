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
        //SubmitReimbursement
        public bool SubmitReimbursement(Tickets newTicket)
        {
            try
            {
                bool s =  new TicketRepostitory().CreateTicket(newTicket);
                if (s)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception)
            {
                throw;

            }
        }
        //UpdateReimbursement
        public bool UpdateReimbursement(Tickets update)
        {
            try
            {
                return new TicketRepostitory().UpdateTicket(update);
            }
            catch (Exception)
            {
                return false;
            }
        }
        //GetReimbursementByID
        public Tickets GetReimbursementByID(int ticketID)
        {
            try
            {
                return new TicketRepostitory().GetTicketsById(ticketID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //GetReimbursementByUserId
        public List<Tickets> GetReimbursementByUserID(int userID)
        {
            try
            {
                return new TicketRepostitory().GetTicketsByAuthor(userID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //GetReimbursementByStatus
        public List<Tickets> GetReimbursementByStatus(Status state)
        {
            try
            {
                return new TicketRepostitory().GetTicketsByStatus(state);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
