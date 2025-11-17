using FluentValidation;
using RailwayReservation.Models;

namespace RailwayReservation.Validators
{
    public class SearchTrainValidator : AbstractValidator<TrainRoute>
    {
        public SearchTrainValidator()
        {
            RuleFor(x => x.Source).NotEmpty().WithMessage("Source must be provided.");

            RuleFor(x => x.Destination).NotEmpty().WithMessage("Destination must be provided.");

            // RuleFor(x => x.DepartureDate).GreaterThanOrEqualTo(DateTime.Now.Date).WithMessage("Departure date must be today or a future date.");
        }
    }
}
