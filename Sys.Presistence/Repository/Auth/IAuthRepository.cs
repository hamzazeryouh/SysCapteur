using Sys.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Presistence.Repository.Auth
{
    public interface IAuthRepository
    {
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<bool> CreateUserAsync(ApplicationUser user, string password);
        Task<ApplicationUser> AuthenticateUserAsync(string email, string password);
    }
}
