using FluentValidation;
using RailwayReservation.Models;

namespace RailwayReservation.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.UserId).NotNull().WithMessage("Username cannot be null").NotEmpty().WithMessage
                ("Username cannot be empty").Length(6, 10).WithMessage("Length of the username must contain 6-10 characters.");

            RuleFor(user => user.Name).NotEmpty().WithMessage("Name cannot be empty").NotNull().WithMessage
                ("Name cannot be null");

            RuleFor(user => user.Email).EmailAddress().WithMessage("Email address is invalid");

            RuleFor(user => user.Phone).Matches(@"^\d{10}$").WithMessage("Phone Number must contains 10 digits");

            RuleFor(user => user.Role).Must(Role => Role == "Admin" || Role == "Customer").WithMessage("User should be a customer or an admin");
        }
    }
}
