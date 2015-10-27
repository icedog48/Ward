using IceLib.Model;
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
    }
}
