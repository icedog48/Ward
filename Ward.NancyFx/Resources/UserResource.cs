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
        
        public virtual string UserName { get; set; }
        
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
