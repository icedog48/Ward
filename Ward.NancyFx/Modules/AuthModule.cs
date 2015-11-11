using Nancy;
using Nancy.ModelBinding;
using Nancy.Authentication.Token;
using Nancy.Responses.Negotiation;

using Ward.NancyFx.Resources;
using Ward.Model;
using Ward.Validation;

using IceLib.NancyFx.Attributes;
using IceLib.Storage;
using IceLib.Validation;
using IceLib.Core.Validation;
using IceLib.NancyFx.Validation;

using System.Linq;

using FluentValidation;
using AutoMapper;
using System.Collections.Generic;
using Nancy.Validation;
using Ward.NancyFx.Resources.Validation;
using Ward.Service.Interfaces;

namespace Ward.NancyFx.Modules
{
    public class AuthModule : WardModule
    {
        private readonly ITokenizer tokenizer;
        private readonly IAuthService authService;
        private readonly UserResourceValidation userResourceValidation;

        public AuthModule(ITokenizer tokenizer,
                          IAuthService authService,
                          UserResourceValidation userResourceValidation) : base("/auth")
        {
            this.tokenizer = tokenizer;
            this.authService = authService;
            this.userResourceValidation = userResourceValidation;
        }

        /// <summary>
        /// POST /auth/login
        /// </summary>
        [Post("/login")]
        public Negotiator Login()
        {
            try
            {
                //Retrieve and validate the resource from request
                var userResource = this.Bind<UserResource>();
                this.userResourceValidation.Validate(userResource).AndThrow();

                //Retrieve the authenticated from the database
                var authenticatedUser = authService.Login(Mapper.Map<User>(userResource));

                //Retrieve the authenticated resource from the model
                var authenticatedUserResource = Mapper.Map<UserResource>(authenticatedUser);

                //Negotiate a response with the client
                return Negotiate
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithModel(new AuthTokenResource()
                        {
                            Token = this.tokenizer.Tokenize(authenticatedUserResource, this.Context)
                        });
            }
            catch (FluentValidation.ValidationException ex)
            {
                var errors = Mapper.Map<IEnumerable<ValidationError>>(ex.Errors);

                return Negotiate
                        .WithStatusCode(HttpStatusCode.BadRequest)
                        .WithModel(errors);
            }
        }
    }
}
