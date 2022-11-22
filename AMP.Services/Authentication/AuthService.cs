namespace AMP.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(LoginQueryObject user)
        {
            var secretKey = _configuration["Jwt:Key"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var claims = new Claim[]
            {
                new(ClaimTypes.Name, user.DisplayName),
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Surname, user.FamilyName),
                new("ImageUrl", user.ImageUrl ?? ""),
                new(ClaimTypes.MobilePhone, user.Contact_PrimaryContact ?? ""),
                new(ClaimTypes.Role, user.Type),
                new(ClaimTypes.Email, user.Contact_EmailAddress ?? ""),
                new(ClaimTypes.StreetAddress, user.Address_StreetAddress ?? ""),
            };

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = user.Type == "Customer" ? DateTime.UtcNow.AddMinutes(20) : DateTime.UtcNow.AddHours(1),
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