using IceLib.Services.Exceptions;
using IceLib.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Service.Exceptions
{
    public class IncorrectPasswordException : ValidationException
    {
        public IncorrectPasswordException() { }

        public IncorrectPasswordException(string message) : base(message) { }

        public IncorrectPasswordException(IEnumerable<ValidationError> errors): base(errors) { }
    }
}
