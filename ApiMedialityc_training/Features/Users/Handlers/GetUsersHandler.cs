using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc_training.Data;
using ApiMedialityc_training.Features.Users.DTOs;
using ApiMedialityc_training.Features.Users.Queries;
using ApiMedialityc_training.Features.Users.Models;
using Microsoft.EntityFrameworkCore;
    using ApiMedialityc_training.Features.Common;

namespace ApiMedialityc_training.Features.Users.Handlers
{
    public class GetUsersHandler 
        : IQueryHandler<GetUsersQuery, PagedResponse<UserResponseDto>>
    {
        private readonly MedialitycDBContext _context;

        public GetUsersHandler(MedialitycDBContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<UserResponseDto>> HandleAsync(
            GetUsersQuery query,
            CancellationToken ct)
        {
            var baseQuery = _context.Users
                .Include(u => u.Emails)
                .Include(u => u.Phones)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.FullName))
                baseQuery = baseQuery.Where(u => u.FullName.Contains(query.FullName));

            if (query.IsActive.HasValue)
                baseQuery = baseQuery.Where(u => u.IsActive == query.IsActive);

            var totalItems = await baseQuery.CountAsync(ct);

            var users = await baseQuery
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Role = u.Role,
                    IsActive = u.IsActive,
                    Emails = u.Emails.Select(e => new UserEmailDto { Email = e.Email }).ToList(),
                    Phones = u.Phones.Select(p => new UserPhoneDto { Phone = p.Phone }).ToList()
                })
                .ToListAsync(ct);

            return new PagedResponse<UserResponseDto>
            {
                Items = users,
                Page = query.Page,
                PageSize = query.PageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)query.PageSize),
                HasNext = query.Page * query.PageSize < totalItems,
                HasPrevious = query.Page > 1
            };
        }
    }
}