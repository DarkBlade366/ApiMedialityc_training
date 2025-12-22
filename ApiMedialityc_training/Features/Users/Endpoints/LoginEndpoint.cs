using System.Threading;
using System.Threading.Tasks;
using ApiMedialityc_training.Features.Users.Commands;
using ApiMedialityc_training.Features.Users.DTOs;
using ApiMedialityc_training.Features.Users.Handlers;
using ApiMedialityc_training.Features.Users.Validations;
using FastEndpoints;

namespace ApiMedialityc_training.Features.Users.Endpoints
{
    public class LoginEndpoint : Endpoint<LoginRequestDto, LoginResponseDto>
    {
        private readonly LoginHandler _handler;

        public LoginEndpoint(LoginHandler handler)
        {
            _handler = handler;
        }

        public override void Configure()
        {
            Post("/login");
            AllowAnonymous();     // Login does not require authentication
            Validator<LoginValidator>();
        }

        public override async Task HandleAsync(LoginRequestDto req, CancellationToken ct)
        {
            var command = new LoginCommand(req.Email, req.Password);

            var result = await _handler.HandleAsync(command, ct);

            if (result == null)
            {
                await Send.UnauthorizedAsync(ct);
                return;
            }

            await Send.OkAsync(result, ct);
        }
    }
}
