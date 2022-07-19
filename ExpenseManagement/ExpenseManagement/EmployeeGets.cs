using CustomExceptions;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using Models;

namespace ConsoleFrontEnd
{
    public class EmployeeGets
    {
        public EmployeeGets() { }
        /// <summary>
        /// This will talk to the website to retreive all tickets that the employee has authored
        /// </summary>
        /// <param name="you">The current user</param>
        /// <param name="api">The website url</param>
        /// <returns>All tickets the user has authored</returns>
        /// <exception cref="ResourceNotFoundException">The user hass not made any tickets</exception>
        public async Task<List<Tickets>> GetAllTickets(Users you, string api)
        {
            var http = new HttpClient();
            HttpResponseMessage result = await http.GetAsync(api + "tickets");
            if((int)result.StatusCode == 202)
            {
                List<Tickets>? tickets = JsonSerializer.Deserialize<List<Tickets>>(await result.Content.ReadAsStringAsync());
                if (tickets != null)
                {
                    List<Tickets>? yours = new();
                    foreach (var ticket in tickets)
                    {
                        if (ticket.author == you.userId)
                        {
                            yours.Add(ticket);
                        }
                    }
                    return yours;
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
        /// This will talk to the website to retreive all tickets that the employee has authored with a specific state
        /// </summary>
        /// <param name="you">The current user</param>
        /// <param name="state">The state the employee wishes to see</param>
        /// <param name="api">The website url</param>
        /// <returns>All tickets with that state and authored by the employee</returns>
        /// <exception cref="ResourceNotFoundException">There are no tickets with that state</exception>
        public async Task<List<Tickets>> GetTicketsByState(Users you, string state, string api)
        {
            var http = new HttpClient();
            HttpResponseMessage result = await http.GetAsync(api + "tickets/status/" + state);
            if((int)result.StatusCode ==202)
            {
                List<Tickets>? tickets = JsonSerializer.Deserialize<List<Tickets>>(await result.Content.ReadAsStringAsync());
                List<Tickets>? yours = new();
                if (tickets != null)
                {
                    foreach (var ticket in tickets)
                    {
                        if (ticket.status == (Status)new Tickets().StateToNum(state) && ticket.author == you.userId)
                        {
                            yours.Add(ticket);
                        }
                    }
                    return yours;
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

