using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PraPdBL_Backend.Data;
using PraPdBL_Backend.DTOs;

namespace PraPdBL_Backend.Validators;

public class BookingCreateDtoValidator : AbstractValidator<BookingCreateDto>
{
    private readonly AppDbContext _dbContext;

    public BookingCreateDtoValidator(AppDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.RoomId)
            .GreaterThan(0);

        RuleFor(x => x.UserId)
            .GreaterThan(0);

        RuleFor(x => x.Purpose)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(x => x.StartTime)
            .NotEmpty();

        RuleFor(x => x.EndTime)
            .NotEmpty()
            .GreaterThan(x => x.StartTime)
            .WithMessage("EndTime harus setelah StartTime.");

        RuleFor(x => x)
            .MustAsync(NoOverlappingBooking)
            .WithMessage("Room sudah terbooking pada rentang waktu tersebut.");
    }

    private async Task<bool> NoOverlappingBooking(BookingCreateDto dto, CancellationToken cancellationToken)
    {
        if (dto.EndTime <= dto.StartTime)
        {
            return true;
        }

        return !await _dbContext.Bookings
            .AsNoTracking()
            .AnyAsync(
                b => b.RoomId == dto.RoomId
                    && b.DeletedAt == null
                    && b.StartTime < dto.EndTime
                    && b.EndTime > dto.StartTime,
                cancellationToken);
    }
}
