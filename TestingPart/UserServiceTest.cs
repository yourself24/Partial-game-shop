using Proj.BLL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Proj.DAL.Models;

namespace TestingPart
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public async Task CreateUser_ValidUser_ReturnsTrue(Task result)
        {
            // Arrange
            var userService = new UserService(/* mock dependencies */);
            var user = new User();
            user.Username = "username";
            user.Password = "password";
            user.Email = "email";
            user.Address = "address";
            user.Id = 244;

            // Act
            await userService.CreateUser(user);
            
            // Assert
        }
    }
}