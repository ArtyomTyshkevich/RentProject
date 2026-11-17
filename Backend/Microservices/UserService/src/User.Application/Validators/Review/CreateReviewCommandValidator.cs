using FluentValidation;
using User.BLL.CQRS.Reviews.Commands;

namespace User.BLL.Validators.Review
{
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(x => x.Score).InclusiveBetween(0, 5);
            RuleFor(x => x.Text).MaximumLength(2000);
            RuleFor(x => x.ReviewerId).NotEmpty();
            RuleFor(x => x.ProsIds)
                .NotNull()
                .Must(ids => ids.Distinct().Count() == ids.Count)
                .WithMessage("ProsIds содержит дубликаты");
        }
    }
}