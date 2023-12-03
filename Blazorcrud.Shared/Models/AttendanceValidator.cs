using FluentValidation;

namespace Blazorcrud.Shared.Models;

public class AttendanceValidator : AbstractValidator<Attendance>
{
    public AttendanceValidator()
    {
        CascadeMode = CascadeMode.Stop;

        // RuleFor(attendance => attendance.DayOfWeek).NotEmpty().WithMessage("Day of week is a required field.");
        RuleFor(attendance => attendance.AttendanceTime).NotEmpty().WithMessage("Attendance time is a required field.");
        RuleFor(attendance => attendance.DepartureTime).NotEmpty().WithMessage("Departure time is a required field.")
            .GreaterThanOrEqualTo(attendance => attendance.AttendanceTime)
            .WithMessage("Departure time must be equal to or later than attendance time.");
    }
}