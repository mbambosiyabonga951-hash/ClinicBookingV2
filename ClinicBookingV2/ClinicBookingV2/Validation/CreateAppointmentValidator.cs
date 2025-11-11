using FluentValidation;
using ClinicBooking.Client.Models;

public sealed class CreateAppointmentValidator : AbstractValidator<CreateAppointmentRequest>
{
    //public CreateAppointmentValidator()
    //{
    //    RuleFor(x => x.ClinicId).GreaterThan(0);
    //    RuleFor(x => x.PatientId).GreaterThan(0);
    //        RuleFor(x => x.Date)
    //        .NotEmpty()
    //        .Must(date => date.HasValue)
    //        .WithMessage("Date must be provided.");
    //    RuleFor(x => x.StartUtc)
    //        .NotEmpty()
    //        .Must(time => time.HasValue)
    //        .WithMessage("StartTime must be provided.");
    //    RuleFor(x => x.EndTime)
    //        .NotEmpty()
    //        .Must(time => time.HasValue)
    //        .WithMessage("EndTime must be provided.");
    //    RuleFor(x => x)
    //        .Must(x =>
    //        {
    //            if (!x.StartUtc.HasValue || !x.EndUtc.HasValue) return false;
    //            return x.StartUtc.Value < x.EndUtc.Value;
    //        })
    //        .WithMessage("StartTime must be before EndTime.");
    //}
}
