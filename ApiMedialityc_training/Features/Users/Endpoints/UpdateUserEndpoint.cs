using System;
using System.Threading;
using System.Threading.Tasks;
using ApiMedialityc_training.Features.Users.Commands;
using ApiMedialityc_training.Features.Users.DTOs;
using ApiMedialityc_training.Features.Users.Handlers;
using FastEndpoints;

namespace ApiMedialityc_training.Features.Users.Endpoints
{
    public class UpdateUserEndpoint : Endpoint<UpdateUserRequestDto, UserResponseDto>
    {
        private readonly UpdateUserHandler _handler;

        public UpdateUserEndpoint(UpdateUserHandler handler)
        {
            _handler = handler;
        }

        public override void Configure()
        {
            Put("/users/{UserId}");
            Roles("Admin");
        }

        public override async Task HandleAsync(UpdateUserRequestDto req, CancellationToken ct)
        {
            var userId = Route<Guid>("UserId"); 
            var command = new UpdateUserCommand(userId, req.FullName, req.Emails, req.Phones);
            var updatedUser = await _handler.HandleAsync(command, ct);
            await Send.OkAsync(updatedUser, ct);
        }
    }
}
