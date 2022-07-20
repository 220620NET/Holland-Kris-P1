using CustomExceptions;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using Models;

namespace ConsoleFrontEnd
{
    public class UserGets
    {
        public async Task<List<Users>> GetAllUsers(string api)
        {
            var http = new HttpClient();
            HttpResponseMessage result = await http.GetAsync(api + "users");
            if ((int)result.StatusCode == 202)
            {
                List<Users>? all = JsonSerializer.Deserialize<List<Users>>(await result.Content.ReadAsStringAsync());
                if (all != null)
                {
                    return all;
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
        public async Task<Users> GetUser(string username,string api)
        {
            var http = new HttpClient();
            HttpResponseMessage result = await http.GetAsync(api + "users/name/"+username);
            if ((int)result.StatusCode == 202)
            {
                Users? all = JsonSerializer.Deserialize<Users>(await result.Content.ReadAsStringAsync());
                if (all != null)
                {
                    return all;
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
        public async Task<Users> GetUser(int userID, string api)
        {
            var http = new HttpClient();
            HttpResponseMessage result = await http.GetAsync(api + "users/name/" + userID);
            if ((int)result.StatusCode == 202)
            {
                Users? all = JsonSerializer.Deserialize<Users>(await result.Content.ReadAsStringAsync());
                if (all != null)
                {
                    return all;
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
