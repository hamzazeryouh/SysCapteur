using Sys.Application.DTO.Auth;
using Sys.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application
{
    public interface IAuthService
    {
        Task<IResponse<string>> LoginAsync(string email, string password);
        Task<IResponse<string>> RegisterAsync(RegisterModel model);
    }
}
