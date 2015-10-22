using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ward.Model;

namespace Ward.Service.Interfaces
{
    public interface IAuthService
    {
        User Login(string username, string password);
    }
}
