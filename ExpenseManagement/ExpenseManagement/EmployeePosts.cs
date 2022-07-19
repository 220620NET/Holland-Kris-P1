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
        public async Task<List<Tickets>> CreateReimbursement(Tickets ticket, string api)
        {
            string serializedUser = JsonSerializer.Serialize(ticket);
            StringContent content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
            HttpClient http = new HttpClient();
            HttpResponseMessage response = await http.PostAsync(api + "submit", content);
            if ((int)response.StatusCode == 202)
            {
                List<Tickets> created = JsonSerializer.Deserialize<List<Tickets>>(await response.Content.ReadAsStringAsync());
                //Console.WriteLine(created);
                return created;
            }
            else if ((int)response.StatusCode == 409)
            {
                Console.WriteLine("That ticket had invalid information please try again.");
                throw new InvalidCredentialsException();
            }
            else
            {
                Console.WriteLine(response.StatusCode);
                throw new UsernameNotAvailable();
            }
        }
    }
}
