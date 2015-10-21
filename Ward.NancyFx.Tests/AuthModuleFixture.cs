using IceLib.Validation;
using Moq;
using Nancy.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Service.Exceptions;
using Ward.Service.Interfaces;
using Xunit;

namespace Ward.NancyFx.Tests
{
    public class AuthModuleFixture
    {
        public AuthModuleFixture()
        {
            
        }

        [Fact(DisplayName = "1 - Should return a token for a valid login")]
        public void Should_return_generated_token_for_valid_user_credentials()
        {
            var authServiceMock = new Mock<IAuthService>();
                authServiceMock.Setup(authService => authService.Login(It.IsAny<Ward.Model.User>()));

            var bootstrapper = new TestBootstrapper();
                bootstrapper.AuthService = authServiceMock.Object;

            var browser = new Browser(bootstrapper);

            // Given, When
            var response = browser.Post("/api/v1/auth/login", (with) =>
            {
                with.HttpRequest();
                with.Accept("application/json");
                with.Header("User-Agent", "Nancy Browser");
                with.FormValue("UserName", "demo");
                with.FormValue("Password", "demo");
            });

            var authResponse = response.Body.DeserializeJson<Models.AuthToken>();

            // Then
            Assert.NotNull(authResponse);
        }

        [Fact(DisplayName = "2 - Should return validation errors for a invalid object")]
        public void Should_return_validation_errors_for_invalid_object()
        {
            var authServiceMock = new Mock<IAuthService>();
                authServiceMock.Setup(authService => authService.Login(It.IsAny<Ward.Model.User>()));

            var bootstrapper = new TestBootstrapper();
                bootstrapper.AuthService = authServiceMock.Object;

            var browser = new Browser(bootstrapper);

            // Given, When
            var response = browser.Post("/api/v1/auth/login", (with) =>
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

        //TODO: Fact 3 - Should return validation errors for a not found login

        //TODO: Fact 4 - Should return validation errors for a incorrect password login
    }
}
