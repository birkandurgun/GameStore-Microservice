using FluentValidation;
using IdentityService.Api.DTOs;

namespace IdentityService.Api.Validators
{
    public class RegisterDTOValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(50).WithMessage("Name cannot exceed 50 characters");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Surname is required")
                .MaximumLength(50).WithMessage("Surname cannot exceed 50 characters");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(3).WithMessage("Username must not be less than 3 characters")
                .MaximumLength(50).WithMessage("Surname cannot exceed 50 characters");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Birth date is required")
                .Must(BeValidDate).WithMessage("Invalid date format")
                .Must(BeInPast).WithMessage("Birth date cannot be in the future")
                .Must(BeOverMinimumAge).WithMessage("You must be at least 13 years old");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Passwords do not match");
        }

        private bool BeValidDate(DateOnly date)
        {
            return !date.Equals(default);
        }

        private bool BeInPast(DateOnly date)
        {
            return date < DateOnly.FromDateTime(DateTime.Today);
        }

        private bool BeOverMinimumAge(DateOnly date)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var age = today.Year - date.Year;

            if (date > today.AddYears(-age)) age--;

            const int minimumAge = 13;
            return age >= minimumAge;
        }
    }
}
