using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Sys.Application;
using Sys.Application.DTO.Auth;
using Sys.Application.Helpers;
using Sys.Domain.Entities.Users;
using SysCapteur.Exceptions;

namespace SysCapteur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(IAuthService authService, UserManager<ApplicationUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // Call the LoginAsync method from AuthService
            var response = await _authService.LoginAsync(model.Email, model.Password);

            // If the response indicates failure, return the appropriate status code and error
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response.Error);
            }

            // Return the generated JWT token if successful
            return StatusCode(response.StatusCode, response.Data);
        }
        public async Task<IActionResult> RegisterUser(RegisterModel model)
        {
            var response = await _authService.RegisterAsync(model);

            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response.Error);
            }

            return StatusCode(response.StatusCode, response.Data);
        }
    }

}
   


