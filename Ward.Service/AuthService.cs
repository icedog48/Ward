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

namespace Ward.Service
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> userRepository;

        public AuthService(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public IQueryable<User> ActiveItems()
        {
            return this.userRepository.Items.Where(user => user.Active);
        }

        public User Login(string username, string password)
        {   
            var user = this.ActiveItems().FirstOrDefault(x => x.UserName == username);

            //TODO: Implement a validation mechanism that dont use magic strings to define messages
            if (user == null) throw new ValidationException("User not found.");

            password = Encryption.GenerateSHA1Hash(GetSignature(username, password));

            if (!user.Password.Equals(password)) throw new ValidationException("Incorrect password.");

            return user;
        }

        protected string GetSignature(string username, string password)
        {
            return username + password;
        }
    }
}
