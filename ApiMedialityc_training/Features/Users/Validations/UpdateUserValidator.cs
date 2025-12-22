using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc_training.Features.Users.DTOs;
using FluentValidation;

namespace ApiMedialityc_training.Features.Users.Validations
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserRequestDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("FullName es obligatorio")
                .MaximumLength(100);

            RuleFor(x => x.Emails)
                .NotEmpty().WithMessage("Debe tener al menos un correo")
                .Must(list => list.All(e => e.Email.Contains("@")))
                .WithMessage("Todos los correos deben ser válidos");

            RuleFor(x => x.Phones)
                .NotEmpty().WithMessage("Debe tener al menos un teléfono");
        }
    }
}