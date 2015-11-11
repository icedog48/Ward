using IceLib.Core.Validation;
using IceLib.Security.Cryptography;
using IceLib.Services.Exceptions;
using IceLib.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Model;
using Ward.Service.Interfaces;
using Ward.Validation;

namespace Ward.Service
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> userRepository;
        private readonly UserValidation userValidation;

        public AuthService(IRepository<User> userRepository,
                           UserValidation userValidation)
        {
            this.userRepository = userRepository;
            this.userValidation = userValidation;
        }

        public User Login(User user)
        {
            this.userValidation
                    .ValidateLogin(user)
                        .AndThrow();

            return this.userRepository
                            .ActiveItems
                                .FirstOrDefault(x => x.UserName == user.UserName);
        }
    }
}
