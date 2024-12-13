using Sys.Application.DTO.Auth;
using SysCapteur.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string email, string password);
        Task<Response<string>> RegisterAsync(RegisterModel model);
    }
}
