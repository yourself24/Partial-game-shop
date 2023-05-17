using Moq;
using Proj.DAL.Models;
using Proj.DAL.Repos.Contracts;
using Xunit;

namespace Proj.BLL.Services.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async Task CreateUser_ValidUser_ShouldCallCreateItem()
        {
            // Arrange
            var mockRepo = new Mock<IGenericRepo<User>>();
            var mockAccountCreationObservable = new Mock<IAccountCreationObservable>();

            var userService = new UserService(mockRepo.Object, mockAccountCreationObservable.Object);

            var user = new User
            {
                Username = "test",
                Password = "password",
                Email = "test@example.com"
            };

            // Act
            await userService.CreateUser(user);

            // Assert
            mockRepo.Verify(repo => repo.CreateItem(user), Times.Once);
        }
        [Fact]
        public void LoginUser_ValidCredentials_ShouldReturnTrue()
        {
            // Arrange
            var mockRepo = new Mock<IGenericRepo<User>>();
            var mockAccountCreationObservable = new Mock<IAccountCreationObservable>();

            var userService = new UserService(mockRepo.Object, mockAccountCreationObservable.Object);


            mockRepo.Setup(repo => repo.FindUser(It.IsAny<string>())).Returns(new User
            {
                Username = "test",
                Password = BCrypt.Net.BCrypt.HashPassword("password"),
                Email = "test@example.com"
            });

            // Act
            var result = userService.LoginUser("test", "password");

            // Assert
            Assert.True(result);
        }
    }
}
