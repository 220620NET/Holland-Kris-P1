using Moq;
using Models;
using CustomExceptions;
using Services;
using DataAccess;
using System;
using Xunit;
using System.Threading.Tasks;
namespace Validation
{
    public class AuthServicesTesting
    {
        /// <summary>
        /// Testing to see if Login method catches improper password for correct usernames
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        [Fact]
        public void InvalidPasswordForLogin()
        {
            var mockedRepo = new Mock<IUserDAO>();
            Users userToAdd = new()
            {
                username = "StarStruck",
                password = "Lover23",
                role = Role.Employee
            };
            Users userToReturn = new()
            {
                userId = 1,
                username = "StarStruck",
                password = "7",
                role = Role.Employee
            };
            mockedRepo.Setup(repo => repo.GetUserByUsername(userToAdd.username)).Returns(userToAdd);
            AuthServices service = new(mockedRepo.Object);
            Assert.Throws<UsernameNotAvailable>(() => service.Register(userToReturn));
        }

        /// <summary>
        /// Testing to see if the Login Method catches unknown usernames
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        [Fact]
        public void UsernameLoginFailure()
        {
            var mockedRepo = new Mock<IUserDAO>();
            Users userToAdd = new()
            {
                username = "StarStruck",
                password = "Lover23",
                role = Role.Employee
            };
            Users userToReturn = new()
            {
                userId = 1,
                username = "StarStrck",
                password = "Lover3",
                role = Role.Employee
            };
            mockedRepo.Setup(repo => repo.GetUserByUsername(userToAdd.username)).Returns(userToAdd);
            mockedRepo.Setup(repo => repo.GetUserByUsername(userToReturn.username)).Throws<UsernameNotAvailable>();
            AuthServices service = new(mockedRepo.Object);
            mockedRepo.Verify(repo => repo.GetUserByUsername(userToReturn.username), Times.Never);
            Assert.Throws<UsernameNotAvailable>(() => service.Register(userToReturn));
        }

        /// <summary>
        /// Testing to see if the Register method catches duplicate usernames
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        [Fact]
        public void DuplicateUser()
        {
            var mockedRepo = new Mock<IUserDAO>();
            Users userToAdd = new()
            {
                username = "StarStruck",
                password = "Lover23",
                role = Role.Employee
            };
            Users userToReturn = new()
            {
                userId = 1,
                username = "StarStruck",
                password = "Lover23",
                role = Role.Employee
            };
            mockedRepo.Setup(repo => repo.GetUserByUsername(userToAdd.username)).Returns(userToReturn);
            AuthServices service =new(mockedRepo.Object);
            Assert.Throws<UsernameNotAvailable>(() => service.Register(userToAdd));
            mockedRepo.Verify(repo=>repo.GetUserByUsername(userToAdd.username),Times.Once());
        }
    }
}