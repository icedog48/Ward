using Nancy.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.NancyFx.Resources
{
    public class UserResource : IUserIdentity
    {
        public UserResource()
        {

        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Username must have a value")]
        public virtual string UserName { get; set; }

        [Required(ErrorMessage = "Password must have a value")]
        public virtual string Password { get; set; }

        public IEnumerable<string> Claims
        {
            get
            {
                return new List<string>();
            }
        }
    }
}
