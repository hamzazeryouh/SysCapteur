using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Sys.Application;
using Sys.Application.DTO.Auth;
using Sys.Domain.Entities.Users;
using SysCapteur.Exceptions;
using SysCapteur.Helpers;

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
        [HttpPost("Login")]
        public async Task<ActionResult<Response<string>>> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                // Return a BadRequest with the validation errors
                var CustomException = new CustomException(1001, "Invalid input data.");
                var Response = new Response<string>(CustomException, 400);
                return BadRequest(Response);
            }

            try
            {
                var token = await _authService.LoginAsync(model.Email, model.Password);

                var Response = new Response<string>(token, 200);
                return Ok(Response);
            }
            catch (Exception ex)
            {
                var CustomException = new CustomException(1000, "An unexpected error occurred.");
                var Response = new Response<string>(CustomException, 500);
                return StatusCode(500, Response);
            }
           
        }
        [HttpPost("register")]
        public async Task<ActionResult<Response<string>>> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                var CustomException = new CustomException(1001, "Invalid input data.");
                var Response = new Response<string>(CustomException, 400);
                return BadRequest(Response);
            }

            try
            {
                // Register user and generate JWT token
                return await _authService.RegisterAsync(model);
            }
            catch (Exception ex)
            {
                var CustomException = new CustomException(1002, ex.Message);
                var Response = new Response<string>(CustomException, 500);
                return StatusCode(500, Response);
            }
        }

    }
   

}
