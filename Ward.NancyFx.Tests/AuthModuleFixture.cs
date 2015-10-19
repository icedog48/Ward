using IceLib.Validation;
using Nancy.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.NancyFx.Models;
using Xunit;

namespace Ward.NancyFx.Tests
{
    public class AuthModuleFixture
    {
        private readonly Browser browser;

        public AuthModuleFixture()
        {
            var bootstrapper = new TestBootstrapper();
            
            this.browser = new Browser(bootstrapper);
        }

        [Fact(DisplayName = "1 - Should return a token for a valid login")]
        public void Should_return_generated_token_for_valid_user_credentials()
        {
            // Given, When
            var response = this.browser.Post("/api/v1/auth/login", (with) =>
            {
                with.HttpRequest();
                with.Accept("application/json");
                with.Header("User-Agent", "Nancy Browser");
                with.FormValue("UserName", "demo");
                with.FormValue("Password", "demo");
            });

            var authResponse = response.Body.DeserializeJson<AuthToken>();

            // Then
            Assert.NotNull(authResponse);
        }

        [Fact(DisplayName = "2 - Should return validation errors for a invalid object")]
        public void Should_return_validation_errors_for_invalid_object()
        {
            // Given, When
            var response = this.browser.Post("/api/v1/auth/login", (with) =>
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
        }
    }
}
