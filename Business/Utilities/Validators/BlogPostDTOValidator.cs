namespace Business.Utilities.Validators
{
    public class BlogPostDTOValidator : AbstractValidator<BlogPostDTO>
    {
        public BlogPostDTOValidator()
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
