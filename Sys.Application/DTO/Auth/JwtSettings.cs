using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.DTO.Auth
{
    public class JwtSettings
    {
        public string? SecretKey { get; set; }
        public string? ExpirationMinutes { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
    }
}
