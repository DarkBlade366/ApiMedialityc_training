using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApiMedialityc_training.Data;
using ApiMedialityc_training.Features.Users.Commands;
using ApiMedialityc_training.Features.Users.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ApiMedialityc_training.Features.Users.Handlers
{
    public class LoginHandler
    {
        private readonly MedialitycDBContext _context;
        private readonly IConfiguration _config;

        public LoginHandler(MedialitycDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<LoginResponseDto?> HandleAsync(
            LoginCommand command,
            CancellationToken ct)
        {
            var user = await _context.Users
                .Include(u => u.Emails)
                .FirstOrDefaultAsync(
                    u => u.Emails.Any(e => e.Email == command.Email),
                    ct);

            // User does not exist or is inactive.
            if (user == null || !user.IsActive)
                return null;

            // Invalid password
            if (!BCrypt.Net.BCrypt.Verify(command.Password, user.Password))
                return null;

            var keyString = _config["Jwt:Key"]
                ?? throw new InvalidOperationException("JWT Key not configured");

            var issuer = _config["Jwt:Issuer"]
                ?? throw new InvalidOperationException("JWT Issuer not configured");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, command.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new LoginResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
