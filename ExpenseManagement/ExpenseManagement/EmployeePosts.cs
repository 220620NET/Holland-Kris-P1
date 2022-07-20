using CustomExceptions;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using Models;


namespace ConsoleFrontEnd
{
    public class EmployeePosts
    {
        public EmployeePosts() { }
        /// <summary>
        /// This will talk to the website to handle submit requests
        /// </summary>
        /// <param name="ticket">Ticket to be submitted</param>
        /// <param name="api">The website url</param>
        /// <returns>All the tickets the user has made</returns>
        /// <exception cref="ResourceNotFoundException">Some truly weird error that should never be able to happen</exception>
        /// <exception cref="InvalidCredentialsException">That ticket had invalid information please try again</exception>
        /// <exception cref="UsernameNotAvailable">Author doesn't exist somehow</exception>
        public async Task<List<Tickets>> CreateReimbursement(Tickets ticket, string api)
        {
            string serializedUser = JsonSerializer.Serialize(ticket);
            StringContent content = new(serializedUser, Encoding.UTF8, "application/json");
            HttpClient http = new();
            HttpResponseMessage response = await http.PostAsync(api + "submit", content);
            if ((int)response.StatusCode == 202)
            {
                List<Tickets>? created = JsonSerializer.Deserialize<List<Tickets>>(await response.Content.ReadAsStringAsync());
                if (created != null)
                {
                    return created;
                }
                else
                {
                    throw new ResourceNotFoundException();
                }
            }
            else if ((int)response.StatusCode == 409)
            { 
                throw new InvalidCredentialsException();
            }
            else
            { 
                throw new UsernameNotAvailable();
            }
        }
    }
}
