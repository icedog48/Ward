using Nancy;
using Nancy.ModelBinding;
using Nancy.Authentication.Token;
using IceLib.NancyFx.Attributes;
using IceLib.NancyFx.Extensions;
using IceLib.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.NancyFx.Models;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Ward.Service.Interfaces;
using Ward.Service.Exceptions;

namespace Ward.NancyFx.Modules
{
    public class AuthModule : WardModule
    {
        private readonly ITokenizer tokenizer;
        private readonly ValidationHelper validationHelper;
        private readonly IAuthService authService;

        public AuthModule(ITokenizer tokenizer, ValidationHelper validationHelper, IAuthService authService) : base("/auth")
        {
            this.tokenizer = tokenizer;
            this.validationHelper = validationHelper;
            this.authService = authService;
        }

        /// <summary>
        /// POST /auth/login
        /// </summary>
        [Post("/login")]
        public dynamic Login()
        {
            var user = this.Bind<User>();

            //Validate front-end input
            if (validationHelper.HasErrors(user))
                return this.ValidationErrorResponse(validationHelper.Errors);

            try
            {
                //TODO: Usar o automapper
                //authService.Login(user);
            }
            catch (IceLib.Service.Exceptions.ValidationException ex)
            {
                this.ValidationErrorResponse(ex.Errors);
            }

            return Negotiate
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithModel(new AuthToken());
        }
    }
}
