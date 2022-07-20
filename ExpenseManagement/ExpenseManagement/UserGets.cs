using CustomExceptions;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using Models;

namespace ConsoleFrontEnd
{
    public class UserGets
    {
        /// <summary>
        /// This will get all users, used by managers
        /// </summary>
        /// <param name="api">The website url</param>
        /// <returns>The completed task and list of users</returns>
        /// <exception cref="ResourceNotFoundException">The database is empty</exception>
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
        /// <summary>
        /// This will get users by username
        /// </summary>
        /// <param name="username">A valid username</param>
        /// <param name="api">The website url</param>
        /// <returns>The completed task and user</returns>
        /// <exception cref="ResourceNotFoundException">There is no user with that username</exception>
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
        /// <summary>
        /// This will get a user with a specific id
        /// </summary>
        /// <param name="userID">A valid id</param>
        /// <param name="api">The website url</param>
        /// <returns>The completed task and the user</returns>
        /// <exception cref="ResourceNotFoundException">There is no user with that id</exception>
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
