using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using ApiMedialityc_training.Features.Users.DTOs;
using ApiMedialityc_training.Features.Users.Commands;
using ApiMedialityc_training.Features.Users.Handlers;
using ApiMedialityc_training.Features.Users.Validations;

namespace ApiMedialityc_training.Features.Users.Endpoints
{
    public class CreateUserEndpoint : Endpoint<CreateUserRequestDto, UserResponseDto>
    {
        private readonly CreateUserHandler _handler;

        public CreateUserEndpoint(CreateUserHandler handler)
        {
            _handler = handler;
        }

        public override void Configure()
        {
            Post("/users");
            Validator<CreateUserValidator>();
            AllowAnonymous(); // Temporal, luego cambia por roles
        }

        public override async Task HandleAsync(CreateUserRequestDto req, CancellationToken ct)
        {
            var command = new CreateUserCommand(req);
            var response = await _handler.HandleAsync(command);
            await Send.OkAsync(response);
        }
    }
}