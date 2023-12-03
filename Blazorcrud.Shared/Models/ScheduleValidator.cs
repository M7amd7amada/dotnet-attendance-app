using FluentValidation;

namespace Blazorcrud.Shared.Models;

public class ScheduleValidator : AbstractValidator<Schedule>
{
    public ScheduleValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(schedule => schedule.StartTime).NotEmpty().WithMessage("Start time is a required field.");
        RuleFor(schedule => schedule.EndTime).NotEmpty().WithMessage("End time is a required field.")
            .GreaterThanOrEqualTo(schedule => schedule.StartTime)
            .WithMessage("End time must be equal to or later than start time.");
    }
}