namespace Business.Utilities.Validators
{
    public class UserLoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public UserLoginDTOValidator()
        {
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
