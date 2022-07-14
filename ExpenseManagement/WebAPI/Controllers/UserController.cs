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
        public List<Users> GetAllUsers()
        {
            return _authServices.GetAllUsers();
        }
        public Users GetUserByID(int id)
        {
            return _authServices.GetUserByuserId(id);
        }
    }
}
