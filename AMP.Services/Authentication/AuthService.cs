using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AMP.Domain.Entities;
using AMP.Domain.Enums;
using AMP.Processors.Authentication;
using AMP.Processors.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AMP.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Users user)
        {
            var secretKey = _configuration["Jwt:Key"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var claims = new Claim[]
            {
                new(ClaimTypes.Name, user.DisplayName),
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Surname, user.FamilyName),
                new("ImageUrl", user.Image?.ImageUrl ?? ""),
                new(ClaimTypes.MobilePhone, user.Contact?.PrimaryContact ?? ""),
                new(ClaimTypes.Role, user.Type.ToString()),
                new(ClaimTypes.Email, user.Contact?.EmailAddress ?? ""),
                new(ClaimTypes.StreetAddress, user.Address?.StreetAddress ?? ""),
            };

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = user.Type == UserType.Customer ? DateTime.UtcNow.AddMinutes(20) : DateTime.UtcNow.AddHours(1),
                IssuedAt = DateTime.UtcNow,
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}