namespace Business.Utilities.Validators
{
    public class UserRegisterDTOValidator : AbstractValidator<RegisterDTO>
    {
        public UserRegisterDTOValidator()
        {
            RuleFor(r => r.FullName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
            RuleFor(r => r.Email)
                .NotNull()
                .NotEmpty()
                .MaximumLength(128)
                .EmailAddress()
                .MinimumLength(5);
            RuleFor(r => r.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(20);
        }
    }
}
