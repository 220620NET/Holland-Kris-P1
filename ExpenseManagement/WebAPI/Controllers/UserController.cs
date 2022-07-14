using Services;
using Models;
using CustomExceptions;
namespace WebAPI.Controllers
{
    public class UserController
    {
        private readonly UserServices _authServices;
        public UserController(UserServices services)
        {
            _authServices = services;
        }
        public IResult GetAllUsers()
        {
            try
            {
                List<Users> users = _authServices.GetAllUsers();
                return Results.Accepted("/users", users);
            }
            catch (ResourceNotFoundException)
            {
                return Results.NotFound("There are no users");
            }
           
        }
        public IResult GetUserByID(int id)
        {

            try
            {
                Users user = _authServices.GetUserByuserId(id);
                return Results.Accepted("/users/{id}", user);
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
        public IResult GetUserByUsername(string username)
        {
            try
            {
                Users user = _authServices.GetUserByUsername(username);
                return Results.Accepted("/users/name/{username}", user);
            } 
            catch (UsernameNotAvailable)
            {
                return Results.BadRequest("That username doesn't exist");
            }
        }
    }
}
