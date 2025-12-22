using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiMedialityc_training.Data;
using ApiMedialityc_training.Features.Users.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ApiMedialityc_training.Features.Users.Handlers
{
    public class DeactivateUserHandler
    {
        private readonly MedialitycDBContext _context;

        public DeactivateUserHandler(MedialitycDBContext context)
        {
            _context = context;
        }

        public async Task<UserResponseDto> HandleAsync(Guid userId, CancellationToken ct)
        {
            var user = await _context.Users
                .Include(u => u.Emails)
                .Include(u => u.Phones)
                .FirstOrDefaultAsync(u => u.Id == userId, ct);

            if (user == null)
                throw new KeyNotFoundException("Usuario no encontrado.");

            user.IsActive = false;
            await _context.SaveChangesAsync(ct);

            return new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Role = user.Role,
                IsActive = user.IsActive,
                Emails = user.Emails.Select(e => new UserEmailDto { Email = e.Email }).ToList(),
                Phones = user.Phones.Select(p => new UserPhoneDto { Phone = p.Phone }).ToList()
            };
        }
    }
}
