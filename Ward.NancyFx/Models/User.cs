using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.NancyFx.Models
{
    public class User
    {
        [Required(ErrorMessage = "Username must have a value")]
        public virtual string Username { get; set; }

        [Required(ErrorMessage = "Password must have a value")]
        public virtual string Password { get; set; }
    }
}
