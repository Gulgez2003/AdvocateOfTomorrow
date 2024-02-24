namespace Business.Utilities.Validators
{
    public class ContactPostDTOValidator : AbstractValidator<ContactUpdateDTO>
    {
        public ContactPostDTOValidator()
        {
            RuleFor(a => a.Title)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
            RuleFor(a => a.ContactInformation)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2);
        }
    }
}
