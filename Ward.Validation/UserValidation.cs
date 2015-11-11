using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Model;
using IceLib.Core.Extensions;
using FluentValidation.Results;
using System.ComponentModel;
using IceLib.Storage;
using IceLib.Security.Cryptography;

namespace Ward.Validation
{
    public abstract class BaseValidator<TModel> : AbstractValidator<TModel>
    {
        public void RuleSet(Enum ruleSetName, Action action)
        {
            this.RuleSet(ruleSetName.ToName(), action);
        }

        public ValidationResult ValidateRulesets(TModel modelInstance, params Enum[] ruleSets)
        {
            var stringRuleSets = string.Join(", ", ruleSets.Select(x => x.ToName()));

            return this.Validate(modelInstance, ruleSet: stringRuleSets);
        }
    }

    public class UserValidation : AbstractValidator<User>
    {
        public enum RuleSets
        {
            [Description("Default")]
            Default,

            [Description("Login")]
            Login
        }

        private readonly IRepository<User> userRepository;

        public UserValidation(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;

            RuleSet(RuleSets.Default.ToName(), () =>
            {
                RuleFor(x => x.UserName)
                    .NotEmpty()
                        .WithMessage("Required !");

                RuleFor(x => x.Password)
                    .NotEmpty()
                        .WithMessage("Required !");
            });

            RuleSet(RuleSets.Login.ToName(), () =>
            {
                RuleFor(x => x.Password)
                    .Must(HaveValidPassworld)
                        .WithMessage("Bad credentials !");
            });                
        }

        protected virtual bool ExistInDatabase(string username)
        {
            return userRepository.ActiveItems.Any(x => x.UserName == username);
        }

        protected virtual bool HaveValidPassworld(User user, string passworld)
        {
            //Check the password
            var savedUser = userRepository.ActiveItems.FirstOrDefault(x => x.UserName == user.UserName);

            if (savedUser == null) return false;

            //Encrypt the password
            user.Password = Encryption.GenerateSHA1Hash(GetSignature(user.UserName, passworld));

            return (savedUser != null) && (savedUser.Password.Equals(user.Password));
        }

        protected virtual string GetSignature(string username, string password)
        {
            return username + password;
        }

        public ValidationResult ValidateLogin(User user)
        {
            var ruleSets = string.Join(",", RuleSets.Default.ToName(), RuleSets.Login.ToName());

            return this.Validate(user, ruleSet: ruleSets);
        }
    }
}
