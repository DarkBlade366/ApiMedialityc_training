using System.Threading;
using System.Threading.Tasks;
using ApiMedialityc_training.Features.Common;
using ApiMedialityc_training.Features.Users.DTOs;
using ApiMedialityc_training.Features.Users.Handlers;
using ApiMedialityc_training.Features.Users.Queries;
using FastEndpoints;

namespace ApiMedialityc_training.Features.Users.Endpoints
{
    public class GetUsersEndpoint : EndpointWithoutRequest<PagedResponse<UserResponseDto>>
    {
        private readonly IQueryHandler<GetUsersQuery, PagedResponse<UserResponseDto>> _handler;

        public GetUsersEndpoint(IQueryHandler<GetUsersQuery, PagedResponse<UserResponseDto>> handler)
        {
            _handler = handler;
        }

        public override void Configure()
        {
            Get("/users");
            Roles("Admin");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var page = Query<int?>("page") ?? 1;
            var pageSize = Query<int?>("pageSize") ?? 10;
            var fullName = Query<string?>("fullName");
            var isActive = Query<bool?>("isActive");

            var query = new GetUsersQuery
            {
                Page = page,
                PageSize = pageSize,
                FullName = fullName,
                IsActive = isActive
            };

            var result = await _handler.HandleAsync(query, ct);
            await Send.OkAsync(result, ct);
        }
    }
}
