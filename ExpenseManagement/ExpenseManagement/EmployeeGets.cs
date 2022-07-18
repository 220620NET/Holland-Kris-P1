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
        public async Task<List<Tickets>> GetAllTickets(Users you, string api)
        {
            var http = new HttpClient();
            HttpResponseMessage result = await http.GetAsync(api + "tickets");
            List<Tickets> tickets = JsonSerializer.Deserialize<List<Tickets>>(await result.Content.ReadAsStringAsync());
            List<Tickets> yours = new List<Tickets>();
            foreach (var ticket in tickets)
            {
                if (ticket.author == you.userId)
                {
                    yours.Add(ticket);
                }
            }
            return yours;
        }
        public async Task<List<Tickets>> GetTicketsByState(Users you, string state, string api)
        {
            var http = new HttpClient();
            HttpResponseMessage result = await http.GetAsync(api + "tickets/status/" + state);
            List<Tickets> tickets = JsonSerializer.Deserialize<List<Tickets>>(await result.Content.ReadAsStringAsync());
            List<Tickets> yours = new List<Tickets>();
            foreach (var ticket in tickets)
            {
                if (ticket.status == (Status)new Tickets().StateToNum(state) && ticket.author == you.userId)
                {
                    yours.Add(ticket);
                }
            }
            return yours;
        }
    }
}
