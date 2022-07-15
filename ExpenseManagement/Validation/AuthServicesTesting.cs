using Models;
using Services;
using CustomExceptions;
namespace Validation
{
    public class AuthServicesTesting
    {
        [Theory]
        [InlineData("Kris","aspd")]
        [InlineData("Kris","apmenols")]
        [InlineData("Kris","tacos")]
        public void InvalidPasswordForLogin(string username, string password)
        {
            Assert.Throws<InvalidCredentialsException>(() => new AuthServices().Login(username, password));
        }

        [Theory]
        [InlineData("Taco", "password")]
        [InlineData("Pizza", "aspd")]
        [InlineData("PrincessLeia", "apmenols")]
        [InlineData("SkywalkerFan_5_4", "tacos")]
        public void UsernameLoginFailure(string username, string password)
        {
            Assert.Throws<ResourceNotFoundException>(()=>new AuthServices().Login(username, password));
        }

        [Theory]
        [InlineData("Kris", "password",1)]
        [InlineData("Kris","as",1)]
        [InlineData("Kris","asdmoam",0)]
        [InlineData("StarCraftEnjoyer","ToInfinityAnd",1)]

        public void DuplicateUser(string username, string password, int role)
        {
            var authServices = new AuthServices();
            Users user = new Users(username, password, role);
            Assert.Throws<UsernameNotAvailable>(() => authServices.Register(user));
        }
    }
}