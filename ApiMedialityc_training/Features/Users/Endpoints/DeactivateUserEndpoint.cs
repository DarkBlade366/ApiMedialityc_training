using System;
using System.Threading;
using System.Threading.Tasks;
using ApiMedialityc_training.Features.Users.DTOs;
using ApiMedialityc_training.Features.Users.Handlers;
using FastEndpoints;

namespace ApiMedialityc_training.Features.Users.Endpoints
{
    public class DeactivateUserEndpoint : EndpointWithoutRequest<UserResponseDto>
    {
        private readonly DeactivateUserHandler _handler;

        public DeactivateUserEndpoint(DeactivateUserHandler handler)
        {
            _handler = handler;
        }

        public override void Configure()
        {
            Put("/users/deactivate/{UserId}");
            Roles("Admin");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var userId = Route<Guid>("UserId");
            var result = await _handler.HandleAsync(userId, ct);
            await Send.OkAsync(result, ct);
        }
    }
}
