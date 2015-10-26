using IceLib.Services.Exceptions;
using IceLib.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ward.Service.Exceptions
{
    public class ResourceNotFoundException : ValidationException
    {
        public ResourceNotFoundException() { }

        public ResourceNotFoundException(string message) : base(message) { }

        public ResourceNotFoundException(IEnumerable<ValidationError> errors): base(errors) { }
    }
}
