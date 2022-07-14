using Services;
using Models;
using CustomExceptions;
namespace WebAPI.Controllers
{
    public class AuthController
    {
        private readonly AuthServices _authServices;
        public AuthController(AuthServices services)
        {
            _authServices = services;
        }
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
        public IResult Login(Users user)
        {
            try
            {  
                user = _authServices.Login(user.username, user.password);
                return Results.Created("/login", user);
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
