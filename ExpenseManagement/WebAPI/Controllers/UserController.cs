using Services;
using Models;
using CustomExceptions;
namespace WebAPI.Controllers
{
    public class UserController
    {
        //Dependency Injection
        private readonly UserServices _Services;
        public UserController(UserServices services)
        {
            _Services = services;
        }
        /// <summary>
        /// Controller that will grab all users 
        /// </summary>
        /// <returns>Status Code 202 if successful</returns>
        /// <remarks>returns Status Code 404 if there are no users in the table, should never be seen</remarks>
        public IResult GetAllUsers()
        {
            try
            {
                List<Users> users = _Services.GetAllUsers();
                return Results.Accepted("/users", users);
            }
            catch (ResourceNotFoundException)
            {
                return Results.NotFound("There are no users");
            }
           
        }
        /// <summary>
        /// Controller to retrieve a particular user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status Code 202 if the user exists and could be retrieved</returns>
        /// <remarks>returns Status Code 400 if there is no user with that userId</remarks>
        public IResult GetUserByID(int id)
        {

            try
            {
                Users user = _Services.GetUserByuserId(id);
                return Results.Accepted("/users/id/{id}", user);
            }
            catch(ResourceNotFoundException )
            {
                return Results.BadRequest("NO user has that ID");
            }
            catch (UsernameNotAvailable)
            {
                return Results.BadRequest("That Id doesn't exist");
            }
        }
        /// <summary>
        /// Controller to retrieve a particular user
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Status Code 202 if the user exists and could be retrieved</returns>
        /// <remarks>returns Status Code 400 if there is no user with that username </remarks>
        public IResult GetUserByUsername(string username)
        {
            try
            {
                Users user = _Services.GetUserByUsername(username);
                return Results.Accepted("/users/name/{username}", user);
            } 
            catch (UsernameNotAvailable)
            {
                return Results.BadRequest("That username doesn't exist");
            }
        }
        public IResult DeleteUser(int id)
        {
            try
            {
                Users user = _Services.DeleteUser(id);
                return Results.Accepted("/fire/{id}", user);
            }
            catch (ResourceNotFoundException)
            {
                return Results.BadRequest("That username doesn't exist");
            }
        }
    }
}
