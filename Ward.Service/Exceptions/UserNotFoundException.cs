using IceLib.Services.Exceptions;
using IceLib.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Service.Exceptions
{
    public class UserNotFoundException : ValidationException
    {
        public UserNotFoundException() { }

        public UserNotFoundException(string message) : base(message) { }

        public UserNotFoundException(IEnumerable<ValidationError> errors): base(errors) { }
    }
}
