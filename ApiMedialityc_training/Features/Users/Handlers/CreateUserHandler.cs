using System;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc_training.Data;
using ApiMedialityc_training.Features.Users.Commands;
using ApiMedialityc_training.Features.Users.DTOs;
using ApiMedialityc_training.Features.Users.Models;
using BCrypt.Net;

namespace ApiMedialityc_training.Features.Users.Handlers
{
    public class CreateUserHandler
    {
        private readonly MedialitycDBContext _context;

        public CreateUserHandler(MedialitycDBContext context)
        {
            _context = context;
        }

        public async Task<UserResponseDto> HandleAsync(CreateUserCommand command)
        {
            var dto = command.Request;

            // Created user
            var user = new User
            {
                FullName = dto.FullName,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "User",
                IsActive = true
            };

            // Emails
            user.Emails = dto.Emails
                .Select(e => new UserEmail 
                { 
                    Email = e.Email,
                    UserId = user.Id
                })
                .ToList();

            // Phones
            user.Phones = dto.Phones
                .Select(p => new UserPhone 
                { 
                    Phone = p.Phone,
                    UserId = user.Id
                })
                .ToList();

            // Add of the DB
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Return DTO
            return new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Role = user.Role,
                IsActive = user.IsActive,
                Emails = user.Emails.Select(e => new UserEmailDto { Email = e.Email }).ToList(),
                Phones = user.Phones.Select(p => new UserPhoneDto { Phone = p.Phone }).ToList(),
            };
        }
    }
}
