namespace Business.Utilities.Validators
{
    public class AboutPostDTOValidator : AbstractValidator<AboutPostDTO>
    {
        public AboutPostDTOValidator()
        {
            RuleFor(a => a.Title)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
            RuleFor(a => a.Description)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2);
        }
    }
}
