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
        public virtual string Email { get; set; }

        public virtual string Password { get; set; }
    }
}
