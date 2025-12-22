using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc_training.Data;
using ApiMedialityc_training.Features.Users.Commands;
using ApiMedialityc_training.Features.Users.DTOs;
using ApiMedialityc_training.Features.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiMedialityc_training.Features.Users.Handlers
{
    public class UpdateUserHandler
    {
        private readonly MedialitycDBContext _context;

        public UpdateUserHandler(MedialitycDBContext context)
        {
            _context = context;
        }

        public async Task<UserResponseDto> HandleAsync(UpdateUserCommand command, CancellationToken ct)
        {
            var user = await _context.Users
                .Include(u => u.Emails)
                .Include(u => u.Phones)
                .FirstOrDefaultAsync(u => u.Id == command.UserId);

            if (user == null)
                throw new KeyNotFoundException("Usuario no encontrado.");

            user.FullName = command.FullName;

            _context.UserEmails.RemoveRange(user.Emails);
            _context.UserPhones.RemoveRange(user.Phones);

            user.Emails = command.Emails
                .Select(e => new UserEmail
                {
                    Email = e.Email,
                    UserId = user.Id
                }).ToList();

            user.Phones = command.Phones
                .Select(p => new UserPhone
                {
                    Phone = p.Phone,
                    UserId = user.Id
                }).ToList();

            await _context.SaveChangesAsync();

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