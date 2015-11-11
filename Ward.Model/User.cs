using IceLib.Model;
using IceLib.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Model
{
    public class User : Entity
    {
        public User()
        {

        }

        public virtual string UserName { get; set; }

        public virtual string Password { get; set; }

        public virtual void EncryptPassword() 
        {
            this.Password = Encryption.GenerateSHA1Hash(GetSignature(this.UserName, this.Password));
        }

        protected virtual string GetSignature(string username, string password)
        {
            return username + password;
        }
    }
}
