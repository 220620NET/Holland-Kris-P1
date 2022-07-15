using Services;
using Models;
using CustomExceptions;
namespace WebAPI.Controllers
{
    public class AuthController
    {
        //Dependency injection
        private readonly AuthServices _authServices;
        public AuthController(AuthServices services)
        {
            _authServices = services;
        }
        /// <summary>
        /// Controller to register a new user
        /// </summary>
        /// <remarks>returns Status Code 400 if the username is banned or in use</remarks>
        /// <param name="user">Users model that is read as a json file</param>
        /// <returns>Status Code 201 if the new user was registered</returns>
        public IResult Register(Users user)
        {
            try
            {
                _authServices.Register(user);
                return Results.Created("/register", user);
            }
            catch (UsernameNotAvailable)
            {
                return Results.BadRequest("That username is not allowed or has already been taken. Please try another.");
            }
        }
        /// <summary>
        /// Controller to login a current user
        /// </summary>
        /// <remarks>return Status Code 401 if the user doesn't exist or if the password was incorrect</remarks>
        /// <param name="user"></param>
        /// <returns>return Status Code 200 if the user is logged in</returns>
        public IResult Login(Users user)
        {
            try
            {  
                user = _authServices.Login(user.username, user.password);
                return Results.Ok("Welcome " +user.ToString());
            }
            catch (InvalidCredentialsException)
            {
                return Results.Unauthorized();
            }
            catch (ResourceNotFoundException)
            {
                return Results.Unauthorized();
            }
        }
    }
}
