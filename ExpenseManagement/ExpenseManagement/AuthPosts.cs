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
                Console.WriteLine("Incorrect username or Password please try again");
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
                Console.WriteLine("Incorrect username or Password please try again. You may already have an account.");
                throw new UsernameNotAvailable();
            }
            else
            {

                Console.WriteLine("You are not connected to the server");
                throw new UsernameNotAvailable();
            }
        }
    }
}
