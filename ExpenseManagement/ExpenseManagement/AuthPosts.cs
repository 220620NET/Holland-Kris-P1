using CustomExceptions;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using Models;


namespace ConsoleFrontEnd
{
    public class AuthPosts
    {
        public AuthPosts() { }
        /// <summary>
        /// This will talk to the website and handle login requests
        /// </summary>
        /// <param name="you">The user trying to login</param>
        /// <param name="api">The website url</param>
        /// <returns>The user successfully logged in</returns>
        /// <exception cref="InvalidCredentialsException">The password was incorrect</exception>
        /// <exception cref="UsernameNotAvailable">There is no user with that username</exception>
        public async Task<Users> Login(Users you, string api)
        {
            string serializedUser = JsonSerializer.Serialize(you);
            StringContent content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
            HttpClient http = new HttpClient();
            HttpResponseMessage response = await http.PostAsync(api + "login", content);
            if ((int)response.StatusCode == 200)
            {
                Users? user = JsonSerializer.Deserialize<Users>(await response.Content.ReadAsStringAsync());
                Console.WriteLine(user);
                return user;
            }
            else if ((int)response.StatusCode == 401)
            { 
                throw new InvalidCredentialsException();
            }
            else
            {
                Console.WriteLine(response.StatusCode);
                throw new UsernameNotAvailable();
            }
        }
        /// <summary>
        /// This will talk to the website to handle register requests
        /// </summary>
        /// <param name="you">The user trying to register</param>
        /// <param name="api">The website url</param>
        /// <returns>The user successfully registered</returns>
        /// <exception cref="UsernameNotAvailable">That username s not allowed</exception>
        public async Task<Users> Register(Users you, string api)
        {
            string serializedUser = JsonSerializer.Serialize(you);
            StringContent content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
            HttpClient http = new HttpClient();
            HttpResponseMessage response = await http.PostAsync(api + "register", content);
            if ((int)response.StatusCode == 201)
            {

                Users? user = JsonSerializer.Deserialize<Users>(await response.Content.ReadAsStringAsync());
                Console.WriteLine($"Successful Registration!\nWelcome {user.role} {user.userId}.");
                return user;
            }
            else if ((int)response.StatusCode == 400)
            { 
                throw new UsernameNotAvailable();
            }
            else
            { 
                throw new UsernameNotAvailable();
            }
        }
        /// <summary>
        /// This allows a user to reset their password
        /// </summary>
        /// <param name="reset">The user and their new password</param>
        /// <param name="api">The website url</param>
        /// <returns>The completed task</returns>
        /// <exception cref="UsernameNotAvailable">There is no user with that id</exception>
        /// <exception cref="ResourceNotFoundException">That is an invalid password</exception>
        public async Task Reset(Users reset, string api)
        {
            string serializedUser = JsonSerializer.Serialize(reset);
            StringContent content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
            HttpClient http = new HttpClient();
            HttpResponseMessage response = await http.PutAsync(api + "reset", content);
            if ((int)response.StatusCode == 201)
            {

                bool? user = JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync());
                if (user!=null)
                {
                    Console.WriteLine($"Password reset to {reset.password}for user {reset.userId}.");

                }
            }
            else if ((int)response.StatusCode == 400)
            { 
                throw new UsernameNotAvailable();
            }
            else
            { 
                throw new ResourceNotFoundException();
            }
        }
        /// <summary>
        /// This allows a manager to change the role of a user
        /// </summary>
        /// <param name="reset">The user to reset</param>
        /// <param name="api">The website url</param>
        /// <returns>The completed task</returns>
        /// <exception cref="UsernameNotAvailable">That user does not exist</exception>
        /// <exception cref="ResourceNotFoundException">That user could not be changed</exception>
        public async Task Payroll(Users reset, string api)
        {
            string serializedUser = JsonSerializer.Serialize(reset);
            StringContent content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
            HttpClient http = new HttpClient();
            HttpResponseMessage response = await http.PutAsync(api + "payroll", content);
            if ((int)response.StatusCode == 201)
            {

                bool? user = JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync());
                if (user != null)
                {
                    Console.WriteLine($"Role change to {reset.password}for user {reset.userId} successful.");

                }
            }
            else if ((int)response.StatusCode == 400)
            { 
                throw new UsernameNotAvailable();
            }
            else
            { 
                throw new ResourceNotFoundException();
            }
        }
    }
}
