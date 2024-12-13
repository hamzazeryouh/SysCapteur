using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sys.Application;
using Sys.Application.DTO.Auth;
using Sys.Domain.Entities.Users;
using Sys.Presistence.Repository.Auth;
using SysCapteur.Exceptions;
using SysCapteur.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Sys.Presistence.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _mapper;
        public AuthService(IAuthRepository authRepository, IConfiguration configuration, IMapper mapper, IOptions<JwtSettings> jwtSettings)
        {
            _authRepository = authRepository;
            _configuration = configuration;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _authRepository.AuthenticateUserAsync(email, password);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid login attempt.");
            }

            return GenerateJwtToken(user);
        }
        public async Task<Response<string>> RegisterAsync(RegisterModel model)
        {
            try
            {
                var validationResults = new List<ValidationResult>();
                var isValid = Validator.TryValidateObject(model, new ValidationContext(model), validationResults, true);

                if (!isValid)
                {
                    var errorMessages = string.Join(", ", validationResults.Select(v => v.ErrorMessage));
                    var error = new CustomException(errorCode: 1004, message: "Validation failed: " + errorMessages);
                    return new Response<string>(error, statusCode: 400);
                }

                var existingUser = await _authRepository.GetUserByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    var error = new CustomException(errorCode: 1002, message: "A user with this email already exists.");
                    return new Response<string>(error, statusCode: 400);
                }

                var user = _mapper.Map<ApplicationUser>(model);

                var isCreated = await _authRepository.CreateUserAsync(user, model.Password);
                if (!isCreated)
                {
                    // If the creation failed, return a response with an error message
                    var error = new CustomException(errorCode: 1003, message: "Error occurred while creating the user.");
                    return new Response<string>(error, statusCode: 400);
                }

                // Generate JWT token for the new user
                var token = GenerateJwtToken(user);

                // Return the response with the JWT token
                return new Response<string>(token, statusCode: 201);
            }
            catch (Exception ex)
            {
                // Handle unexpected errors and return a generic error response
                var error = new CustomException(errorCode: 1000, message: "An unexpected error occurred: " + ex.Message);
                return new Response<string>(error, statusCode: 500);
            }
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("FirstName", user.FirstName),
            new Claim("LastName", user.LastName),
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(int.Parse(_jwtSettings.ExpirationMinutes));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

