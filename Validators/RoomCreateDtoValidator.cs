using FluentValidation;
using PraPdBL_Backend.DTOs;

namespace PraPdBL_Backend.Validators;

public class RoomCreateDtoValidator : AbstractValidator<RoomCreateDto>
{
    public RoomCreateDtoValidator()
    {
        RuleFor(x => x.RoomCode)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.RoomName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Capacity)
            .GreaterThan(0);

        RuleFor(x => x.Location)
            .NotEmpty()
            .MaximumLength(120);
    }
}
