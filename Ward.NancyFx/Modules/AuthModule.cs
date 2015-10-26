using Nancy;
using Nancy.Authentication.Token;

using Ward.NancyFx.Models;
using Ward.Service.Interfaces;
using Ward.Service.Exceptions;
using Ward.Model;

using IceLib.NancyFx.Attributes;
using IceLib.Services.Exceptions;
using IceLib.NancyFx.Binding;

namespace Ward.NancyFx.Modules
{
    public class AuthModule : WardModule
    {
        private readonly ITokenizer tokenizer;
        private readonly IAuthService authService;
        private readonly BindHelper<UserViewModel, User> userBinder;

        public AuthModule(ITokenizer tokenizer,
                          BindHelper<UserViewModel, User> userBinder,
                          IAuthService authService) : base("/auth")
        {
            this.tokenizer = tokenizer;
            this.userBinder = userBinder;
            this.authService = authService;
        }

        /// <summary>
        /// POST /auth/login
        /// </summary>
        [Post("/login")]
        public dynamic Login()
        {
            try
            {
                var user = this.userBinder.BindValidWith(this);
                
                var authenticatedUser = authService.Login(user.Username, user.Password);

                return Negotiate
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithModel(new AuthTokenViewModel());
            }
            catch (ResourceNotFoundException)
            {
                return Negotiate
                        .WithStatusCode(HttpStatusCode.BadRequest)
                        .WithModel("User not found");
            }
            catch (IncorrectPasswordException)
            {
                return Negotiate
                        .WithStatusCode(HttpStatusCode.BadRequest)
                        .WithModel("Incorrect password");
            }
            catch (AttributeValidationException ex)
            {
                return Negotiate
                        .WithStatusCode(HttpStatusCode.BadRequest)
                        .WithModel(ex.Errors);
            }
        }
    }
}
