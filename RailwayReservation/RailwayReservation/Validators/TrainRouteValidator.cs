using FluentValidation;
using RailwayReservation.Models;

namespace RailwayReservation.Validators
{
    public class TrainRouteValidator : AbstractValidator<TrainRoute>
    {
        public TrainRouteValidator()
        {
            // Rule for RouteId: Must not be empty
            RuleFor(x => x.RouteId)
                .NotEmpty().WithMessage("RouteId cannot be empty");

            // Rule for Source: Must not be null or empty
            RuleFor(x => x.Source)
                .NotEmpty().WithMessage("Source cannot be empty");

            // Rule for Destination: Must not be null or empty
            RuleFor(x => x.Destination)
                .NotEmpty().WithMessage("Destination cannot be empty");

            // Rule for Distance: Must be greater than 0
            RuleFor(x => x.Distance)
                .GreaterThan(0).WithMessage("Distance must be greater than 0");

            // Rule for Duration: Must not be empty
            RuleFor(x => x.Duration)
                .NotEmpty().WithMessage("Duration cannot be empty");

            // Rule for Departure and Arrival: Departure should be before Arrival
            RuleFor(x => new { x.Departure, x.Arrival })
                .Must(x => x.Departure == null || x.Arrival == null || x.Departure < x.Arrival)
                .WithMessage("Departure must be before Arrival");
        }
    }
}

