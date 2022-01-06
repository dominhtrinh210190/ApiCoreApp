using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Authentication
{ 
    public interface IJwtAuth
    {
        string Authentication(string username, string password);
        string Authentication2(string username, string password);
    }
}
