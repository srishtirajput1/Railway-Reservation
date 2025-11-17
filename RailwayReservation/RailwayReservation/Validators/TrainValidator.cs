using FluentValidation;
using RailwayReservation.Models;
public class TrainValidator : AbstractValidator<Train>
{
    public TrainValidator()
    {
        RuleFor(x => x.TrainId).NotEmpty().WithMessage("TrainId is required.");
        RuleFor(x => x.TrainNumber).NotEmpty().WithMessage("TrainNumber is required.");
        RuleFor(x => x.TrainName).NotEmpty().WithMessage("TrainName is required.");
        RuleFor(x => x.TrainRoute).NotEmpty().WithMessage("Route is required.");
        RuleFor(x => x.RunningDay).Matches(@"^(Sunday|Monday|Tuesday|Wednesday|Thursday|Friday|Saturday)$")
            .When(x => x.RunningDay != null).WithMessage("RunningDay should be a valid day of the week.");
    }
}


