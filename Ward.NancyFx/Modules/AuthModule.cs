using Nancy;
using Nancy.Authentication.Token;

using Ward.NancyFx.Resources;
using Ward.Service.Interfaces;
using Ward.Model;

using IceLib.NancyFx.Attributes;
using IceLib.Services.Exceptions;
using IceLib.NancyFx.Binding;
using IceLib.Storage;

using System.Linq;
using IceLib.Security.Cryptography;
using System;

namespace Ward.NancyFx.Modules
{
    public class AuthModule : WardModule
    {
        private readonly ITokenizer tokenizer;
        private readonly IAuthService authService;
        private readonly BindHelper<UserResource, User> userBinder;

        public AuthModule(ITokenizer tokenizer,
                          BindHelper<UserResource, User> userBinder,
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
                //Retrieve and validate the resource from request
                var user = this.userBinder.BindResource(this);

                //Retrieve and validate the user credentials
                var authenticatedUser = authService.Login(user.UserName, user.Password);

                //Map the authenticated user model to autenticated user resource
                var authenticatedUserResource = userBinder.BindResource(authenticatedUser);

                //Negotiate a response with the client
                return Negotiate
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithModel(new AuthTokenResource()
                        {
                            Token = this.tokenizer.Tokenize(authenticatedUserResource, this.Context)
                        });
            }
            catch (ValidationException ex)
            {
                //TODO: Map the Service Exceptions to Nancy Exceptions (exceptions with correct httpStatusCode)
                return Negotiate
                        .WithStatusCode(HttpStatusCode.BadRequest)
                        .WithModel(ex.Errors);
            }
        }
    }
}
