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
        public async Task<Tickets> UpdateReimbursement(Tickets update, string api)
        {
            string serializedUser = JsonSerializer.Serialize(update);
            StringContent content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
            HttpClient http = new HttpClient();
            HttpResponseMessage response = await http.PostAsync(api + "process", content);
            if ((int)response.StatusCode == 200)
            {
                Tickets updated = JsonSerializer.Deserialize<Tickets>(await response.Content.ReadAsStringAsync());
                Console.WriteLine(updated);
                return updated;
            }
            else if ((int)response.StatusCode == 409)
            {
                Console.WriteLine("Incorrect username or Password please try again");
                throw new InvalidCredentialsException();
            }
            else if ((int)response.StatusCode==400)
            {
                Console.WriteLine("That ticket has already been processed");
                throw new ResourceNotFoundException();
            }
        }
    }
}
