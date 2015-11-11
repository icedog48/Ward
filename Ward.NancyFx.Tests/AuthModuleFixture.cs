using IceLib.Storage;
using IceLib.Validation;
using Moq;
using Nancy.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using Ward.Model;
using Xunit;

namespace Ward.NancyFx.Tests
{
    public class AuthModuleFixture
    {
        private Browser browser;

        public AuthModuleFixture()
        {
            var userRepositoryMock = new Mock<IRepository<User>>();

            var fakeUser = new User()
            {
                Id = 1,
                UserName = "admin",
                Password = "dd94709528bb1c83d08f3088d4043f4742891f4f" //Password == admin 
            }; 

            var users = new List<User>() { fakeUser };

            userRepositoryMock.Setup(x => x.Items).Returns(users.AsQueryable());
            userRepositoryMock.Setup(x => x.ActiveItems).Returns(users.AsQueryable());

            var bootstrapper = new TestBootstrapper();
                bootstrapper.UserRepository = userRepositoryMock.Object;

            browser = new Browser(bootstrapper);
        }

        [Fact(DisplayName = "1 - Should return a token for a valid login")]
        public void Should_return_generated_token_for_valid_user_credentials()
        {
            // Given, When
            var response = this.browser.Post("/api/auth/login", (with) =>
            {
                with.HttpRequest();
                with.Accept("application/json");
                with.Header("User-Agent", "Nancy Browser");
                with.FormValue("UserName", "admin");
                with.FormValue("Password", "admin");
            });

            var authResponse = response.Body.DeserializeJson<Resources.AuthTokenResource>();

            // Then
            Assert.NotNull(authResponse);
        }

        [Fact(DisplayName = "2 - Should return validation errors for a invalid object")]
        public void Should_return_validation_errors_for_invalid_object()
        {
            // Given, When
            var response = this.browser.Post("/api/auth/login", (with) =>
            {
                with.HttpRequest();
                with.Accept("application/json");
                with.Header("User-Agent", "Nancy Browser");
                with.FormValue("UserName", "");
                with.FormValue("Password", "");
            });

            var errorResponse = response.Body.DeserializeJson<IEnumerable<ValidationError>>();
            
            // Then
            Assert.NotNull(errorResponse);

            Assert.True(errorResponse.Any(x => x.MemberName.Equals("UserName", StringComparison.InvariantCultureIgnoreCase)));
            Assert.True(errorResponse.Any(x => x.MemberName.Equals("Password", StringComparison.InvariantCultureIgnoreCase)));
        }
        
        [Fact(DisplayName = "3 - Should return validation errors for a not found login")]
        public void Should_return_validation_errors_for_not_found_login()
        {
            // Given, When
            var response = this.browser.Post("/api/auth/login", (with) =>
            {
                with.HttpRequest();
                with.Accept("application/json");
                with.Header("User-Agent", "Nancy Browser");
                with.FormValue("UserName", "username");
                with.FormValue("Password", "password");
            });

            var errorResponse = response.Body.DeserializeJson<IEnumerable<ValidationError>>();

            // Then
            Assert.NotNull(errorResponse);
        }

        //TODO: Fact 4 - Should return validation errors for a incorrect password login
    }
}
