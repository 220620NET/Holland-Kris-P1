using CustomExceptions;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using Models;

namespace ConsoleFrontEnd
{
    public class ManagerPosts
    {
        public ManagerPosts() { }
        /// <summary>
        /// This will talk to the website to update the specifed ticket
        /// </summary>
        /// <param name="update">The ticket information to update</param>
        /// <param name="api">The website url</param>
        /// <returns>Ticket that was updated</returns>
        /// <exception cref="ResourceNotFoundException">If the ticket that is specified has already been updated</exception>
        /// <exception cref="InvalidCredentialsException">There was improper data entered</exception>
        public async Task<Tickets> UpdateReimbursement(Tickets update, string api)
        {
            string serializedUser = JsonSerializer.Serialize(update);
            StringContent content = new(serializedUser, Encoding.UTF8, "application/json");
            HttpClient http = new();
            HttpResponseMessage response = await http.PutAsync(api + "process", content);
            if ((int)response.StatusCode == 202)
            {
                Tickets? updated = JsonSerializer.Deserialize<Tickets>(await response.Content.ReadAsStringAsync());
                return updated!=null? updated :throw new ResourceNotFoundException();              
            }
            else if ((int)response.StatusCode == 409)
            {
                
                throw new InvalidCredentialsException();
            }
            else if ((int)response.StatusCode==400)
            {
                
                throw new ResourceNotFoundException();
            }
            throw new ResourceNotFoundException();
        }
    }
}
