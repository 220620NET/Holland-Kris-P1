using Services;
using Models;
using CustomExceptions;
namespace WebAPI.Controllers
{
    public class AuthController
    {
        //Dependency injection
        private readonly AuthServices _authServices;
        private readonly UserServices _userServices;
        public AuthController(AuthServices services, UserServices userServices)
        {
            _authServices = services;
            _userServices = userServices;   
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
                user.username = user.username != null ? user.username : "";
                _authServices.Register(user);
                user =_userServices.GetUserByUsername(user.username);
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
                user.username = user.username != null ? user.username : "";
                user.password = user.password != null ? user.password : "";
                user = _authServices.Login(user.username, user.password);
                return Results.Ok(user);
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
        /// <summary>
        /// Controller to reset the password of a user
        /// </summary>
        /// <remarks>returns Status Code 400 if the information is invalid</remarks>
        /// <param name="reset">The user to reset the password of</param>
        /// <returns>Status Code 200</returns>
        public IResult Reset(Users reset)
        {
            try
            {
                _authServices.Reset(reset);
                return Results.Ok(true);
            }
            catch (ResourceNotFoundException)
            {
                return Results.BadRequest();
            }
        }
        /// <summary>
        /// Controller to change the role of a user at the company
        /// </summary>
        /// <remarks>returns Status Code 400 if the information is invalid</remarks>
        /// <param name="reset">The user to change the role of</param>
        /// <returns>Status Code 200</returns>
        public IResult PayRollChange(Users reset)
        {
            try
            {
                _authServices.PayRollChange(reset);
                return Results.Ok(true);
            }
            catch (ResourceNotFoundException)
            {
                return Results.BadRequest();
            }
        }
    }
}
