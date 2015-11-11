using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ward.NancyFx.Resources.Validation
{
    public class UserResourceValidation : AbstractValidator<UserResource>
    {
        public UserResourceValidation()
        {
            RuleFor(x => x.UserName)
                    .NotEmpty()
                        .WithMessage("Required !");

            RuleFor(x => x.Password)
                .NotEmpty()
                    .WithMessage("Required !");
        }
    }
}