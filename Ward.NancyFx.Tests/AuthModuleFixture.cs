using IceLib.Security.Cryptography;
using IceLib.Storage;
using IceLib.Validation;
using Moq;
using Nancy.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Model;
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
            // Given, When
            var response = this.Browser.Post("/api/v1/auth/login", (with) =>
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
            // Given, When
            var response = this.Browser.Post("/api/v1/auth/login", (with) =>
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
            var response = this.Browser.Post("/api/v1/auth/login", (with) =>
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

            Assert.True(errorResponse.Any(x => x.MemberName.Equals("UserName", StringComparison.InvariantCultureIgnoreCase)));
            Assert.True(errorResponse.Any(x => x.MemberName.Equals("Password", StringComparison.InvariantCultureIgnoreCase)));
        }

        public Browser Browser
        {
            get
            {
                var authServiceMock = new Mock<IRepository<User>>();

                var bootstrapper = new TestBootstrapper();
                    bootstrapper.UserRepository = authServiceMock.Object;
                
                return new Browser(bootstrapper); 
            }
        }

        //TODO: Fact 4 - Should return validation errors for a incorrect password login
    }
}
