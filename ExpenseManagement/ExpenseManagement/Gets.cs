using CustomExceptions;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using Models;


namespace ConsoleFrontEnd
{
    public class Gets
    {
        public Gets() { }
        /// <summary>
        /// This will talk to the website to grab all tickets with a specifed state
        /// </summary>
        /// <param name="state">State that is of interest</param>
        /// <param name="api">The website url</param>
        /// <returns>The tickets that have a specified state</returns>
        /// <exception cref="ResourceNotFoundException">There are no tickets with that state</exception>
        public async Task<List<Tickets>> GetTicketsByState(string state, string api)
        {
            var http = new HttpClient();
            HttpResponseMessage result = await http.GetAsync(api + "tickets/status/" + state);
            if ((int)result.StatusCode == 202)
            {
                List<Tickets>? tickets = JsonSerializer.Deserialize<List<Tickets>>(await result.Content.ReadAsStringAsync());
                if (tickets != null)
                {
                    return tickets;
                }
                else
                {
                    throw new ResourceNotFoundException();
                }
            }
            else
            {
                throw new ResourceNotFoundException();
            }
        }
        /// <summary>
        /// This will talk to the website to grab all tickets
        /// </summary>
        /// <param name="api">The website url</param>
        /// <returns>All Tickets from every employee</returns>
        /// <exception cref="ResourceNotFoundException">There are no tickets in the database</exception>
        public async Task<List<Tickets>> GetAllTickets(string api)
        {
            var http = new HttpClient();
            HttpResponseMessage result = await http.GetAsync(api + "tickets");
            if ((int)result.StatusCode == 202)
            {
                List<Tickets>? tickets = JsonSerializer.Deserialize<List<Tickets>>(await result.Content.ReadAsStringAsync());
                if (tickets != null)
                {
                    return tickets;
                }
                else
                {
                    throw new ResourceNotFoundException();
                }
            }
            else
            {
                throw new ResourceNotFoundException();
            }
        }
        /// <summary>
        /// This will talk to the website to grab a specifed ticket
        /// </summary>
        /// <param name="ticketNum">The number for the specific ticket</param>
        /// <param name="api">The website url</param>
        /// <returns>The requested ticket</returns>
        /// <exception cref="ResourceNotFoundException">That ticket hasn't been created yet</exception>
        public async Task<Tickets> GetTicketsByTicketNum( int ticketNum, string api)
        {
            var http = new HttpClient();
            HttpResponseMessage result = await http.GetAsync(api + "tickets/id/" + ticketNum);
            if ((int)result.StatusCode == 202)
            {
                Tickets? tickets = JsonSerializer.Deserialize<Tickets>(await result.Content.ReadAsStringAsync());
                if (tickets != null)
                {
                    return tickets;
                }
                else
                {
                    throw new ResourceNotFoundException();
                }
            }
            else
            {
                throw new ResourceNotFoundException();
            }           
        }
        /// <summary>
        /// This will talk to the website to grab all tickets with a specified author
        /// </summary>
        /// <param name="author">The specified employee's userID</param>
        /// <param name="api">The website url</param>
        /// <returns></returns>
        /// <exception cref="ResourceNotFoundException">That employee hasn't made any tickets yet</exception>
        public async Task<List<Tickets>> GetTicketsByAuthor(int author, string api)
        {
            var http = new HttpClient();
            HttpResponseMessage result = await http.GetAsync(api + "tickets/author/" + author);
            if ((int)result.StatusCode == 202)
            {
                List<Tickets>? tickets = JsonSerializer.Deserialize<List<Tickets>>(await result.Content.ReadAsStringAsync());
                if (tickets != null)
                {
                    return tickets;
                }
                else
                {
                    throw new ResourceNotFoundException();
                }
            }
            else
            {
                throw new ResourceNotFoundException();
            }            
        }        
    }
}
