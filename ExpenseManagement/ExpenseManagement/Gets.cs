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
        public async Task<List<Tickets>> GetTicketsByState(string state, string api)
        {
            var http = new HttpClient();
            HttpResponseMessage result = await http.GetAsync(api + "tickets/status/" + state);
            if ((int)result.StatusCode == 202)
            {
                List<Tickets> tickets = JsonSerializer.Deserialize<List<Tickets>>(await result.Content.ReadAsStringAsync());
                return tickets;
            }
            else
            {
                throw new ResourceNotFoundException();
            }
        }

        public async Task<List<Tickets>> GetAllTickets(string api)
        {
            var http = new HttpClient();
            HttpResponseMessage result = await http.GetAsync(api + "tickets");
            if ((int)result.StatusCode == 202)
            {
                List<Tickets> tickets = JsonSerializer.Deserialize<List<Tickets>>(await result.Content.ReadAsStringAsync());
                return tickets;
            }
            else
            {
                throw new ResourceNotFoundException();
            }
        }
        public async Task<Tickets> GetTicketsByTicketNum( int ticketNum, string api)
        {
            var http = new HttpClient();
            HttpResponseMessage result = await http.GetAsync(api + "tickets/id/" + ticketNum);
            if ((int)result.StatusCode == 202)
            {
                Tickets tickets = JsonSerializer.Deserialize<Tickets>(await result.Content.ReadAsStringAsync());
                return tickets;
            }
            else
            {
                throw new ResourceNotFoundException();
            }
           
        }
        public async Task<List<Tickets>> GetTicketsByAuthor(int author, string api)
        {
            var http = new HttpClient();
            HttpResponseMessage result = await http.GetAsync(api + "tickets/author/" + author);
            if ((int)result.StatusCode == 202)
            {
                List<Tickets> tickets = JsonSerializer.Deserialize<List<Tickets>>(await result.Content.ReadAsStringAsync());
                return tickets;
            }
            else
            {
                throw new ResourceNotFoundException();
            }            
        }        
    }
}
